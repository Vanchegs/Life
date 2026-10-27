using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Reflection;
using Codebase;
using TMPro;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = UnityEngine.Object;

[InitializeOnLoad]
public static class CodeReviewValidation
{
    private static int frames;
    private const string Pending = "CodeReviewValidation.Pending";
    private const string Stage = "CodeReviewValidation.Stage";

    static CodeReviewValidation()
    {
        if (SessionState.GetBool(Pending, false))
            EditorApplication.update += Update;
    }

    public static void Run()
    {
        Require(Application.productName.StartsWith("LifeValidation-"), "Run Tools/ValidateProject.ps1 to isolate save files");
        PlayerDataSave.ClearSaveFile();
        EditorSceneManager.OpenScene("Assets/Scenes/GameScene.unity");
        SessionState.SetInt(Stage, 0);
        SessionState.SetInt("LossCount", 0);
        SessionState.SetBool(Pending, true);
        EditorApplication.update -= Update;
        EditorApplication.update += Update;
        EditorApplication.isPlaying = true;
    }

    private static void Update()
    {
        if (!EditorApplication.isPlaying || EditorApplication.isCompiling || ++frames < 10)
            return;

        try
        {
            switch (SessionState.GetInt(Stage, 0))
            {
                case 0:
                    ValidateNewGame();
                    ValidateSaves();
                    ValidatePurchases();
                    ValidateWorkAndAnimations();
                    ValidateView(Find<WalletView>(), "balanceView", () => GameEventBus.RaiseUpdateBalance(77), () => GameEventBus.RaiseUpdateBalance(88));
                    var stats = Find<StatsController>();
                    stats.SetSavedStats(new SaveData(stats.MaxValue));
                    Find<WalletController>().GetWallet().SetSaveBalance(100);
                    Find<DaysCounter>().SetSavedDay(7);
                    GameEventBus.RaiseSaveGame();
                    Next(1, "CasinoScene");
                    break;
                case 1:
                    ValidateCasino();
                    Next(2, "GameScene");
                    break;
                case 2:
                    Require(Find<DaysCounter>().DayNumber == 7, "Saved day was not restored");
                    Require(Find<WalletController>().GetWallet().GetBalance() == SessionState.GetInt("CasinoBalance", -1), "Casino balance did not survive scene transition");
                    Require(Find<StatsController>().HealthStat > 98, "Saved stats were not restored");
                    StopSimulation();
                    ValidateThoughts();
                    ValidateEventChoices();
                    GameEventBus.OnLossGame += CountLoss;
                    TriggerLoss(false, 3);
                    break;
                case 3:
                    ValidateFinal(false, 1);
                    PlayerDataSave.Save(new SaveData());
                    Next(4, "GameScene");
                    break;
                case 4:
                    TriggerLoss(true, 5);
                    break;
                case 5:
                    ValidateFinal(true, 2);
                    SessionState.SetBool(Pending, false);
                    EditorApplication.update -= Update;
                    PlayerDataSave.ClearSaveFile();
                    Debug.Log("CODE_REVIEW_VALIDATION_PASSED");
                    EditorApplication.Exit(0);
                    break;
            }
        }
        catch (Exception exception)
        {
            SessionState.SetBool(Pending, false);
            EditorApplication.update -= Update;
            Debug.LogException(exception);
            EditorApplication.Exit(1);
        }
    }

    private static void ValidateNewGame()
    {
        var stats = Find<StatsController>();
        Require(!stats.IsGameOver && stats.HealthStat > 98 && stats.FoodStat > 98 && stats.EnergyStat > 98 && stats.MentalStat > 98, "Fresh game has invalid starting stats");
        Require(Find<DaysCounter>().DayNumber == 1, "Fresh game should start on day one");
        StopSimulation();
        Debug.Log("VALIDATED: new game initialization");
    }

    private static void StopSimulation()
    {
        foreach (var behaviour in Object.FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include))
        {
            if (behaviour is StatsController || behaviour is StateSwitcher || behaviour is DaysCounter || behaviour is EventController)
                behaviour.enabled = false;
            if (behaviour is DaysCounter || behaviour is EventController || behaviour is ThoughtController)
                behaviour.StopAllCoroutines();
        }
    }

    private static void ValidateSaves()
    {
        PlayerDataSave.ClearSaveFile();
        var casino = new CasinoWallet();
        casino.GetSavedBalance();
        Require(casino.Balance == 0, "Casino cannot load a missing save");
        casino.SaveBalance();
        Require(PlayerDataSave.Load<SaveData>().HealthStat == 100, "Casino created invalid default stats");
        Require(PlayerDataSave.Save(new SaveData { Balance = 120, DayNumber = 7 }), "Initial save failed");
        Require(PlayerDataSave.Save(new SaveData { Balance = 150, DayNumber = 8 }), "Replacement save failed");
        var path = Path.Combine(Application.persistentDataPath, "SavePlayerData.json");
        Require(File.Exists(path + ".bak"), "Backup was not created");
        File.WriteAllText(path, "{broken");
        Require(PlayerDataSave.Load<SaveData>().Balance == 120, "Backup recovery failed");
        var temporaryPath = path + ".tmp";
        Directory.CreateDirectory(temporaryPath);
        int successLogs = 0;
        Application.LogCallback onLog = (message, trace, type) => { if (message == "Game saved successfully") successLogs++; };
        Application.logMessageReceived += onLog;
        try
        {
            Require(!PlayerDataSave.Save(new SaveData()), "Failed write reported success");
            GameEventBus.RaiseSaveGame();
            Require(successLogs == 0, "Saver reported success after failed write");
            Require(PlayerDataSave.Load<SaveData>().Balance == 120, "Failed write damaged backup");
        }
        finally
        {
            Application.logMessageReceived -= onLog;
            Directory.Delete(temporaryPath);
        }
        PlayerDataSave.ClearSaveFile();
        Debug.Log("VALIDATED: missing save, atomic replacement, backup recovery, write failure");
    }

    private static void ValidatePurchases()
    {
        var stats = Find<StatsController>();
        var wallet = Find<WalletController>().GetWallet();
        stats.SetSavedStats(new SaveData { FoodStat = 50 });
        wallet.SetSaveBalance(0);
        var item = ScriptableObject.CreateInstance<ShopItem>();
        item.price = 10;
        item.foodChangeValue = 20;
        Find<ShopController>().BuyClick(item);
        Require(wallet.GetBalance() == 0 && stats.FoodStat == 50, "Purchase succeeded without money");
        wallet.ChangeBalance(10);
        Find<ShopController>().BuyClick(item);
        Require(wallet.GetBalance() == 0 && stats.FoodStat == 70, "Valid purchase failed");
        item.price = -10;
        Find<ShopController>().BuyClick(item);
        Require(wallet.GetBalance() == 0 && stats.FoodStat == 70, "Negative price was accepted");
        wallet.ChangeBalance(-10);
        Require(wallet.GetBalance() == 0, "Wallet allowed debt");
        Object.Destroy(item);
        var stat = new Stat(StatType.FoodStat, 100, 1, 1);
        stat.SetSavedValue(200);
        Require(stat.GetCurrentStat() == 100, "Loaded stat was not clamped");
        stat.SetSavedValue(float.NaN);
        Require(stat.GetCurrentStat() == 100, "Non-finite saved stat was accepted");
        Debug.Log("VALIDATED: purchase rejection, successful purchase, wallet and stat boundaries");
    }

    private static void ValidateWorkAndAnimations()
    {
        var switcher = Find<StateSwitcher>();
        var machine = Get<StateMachine>(switcher, "stateMachine");
        Set(machine.GetState<WorkState>(), "workProgress", 4f);
        switcher.ChangeStateOnWork();
        var view = Find<WorkingView>();
        Require(Mathf.Abs(Get<Image>(view, "fillImage").fillAmount - 0.5f) < 0.001f, "Resumed work progress is not normalized");
        var names = new[] { "workAnim_0", "playAnim_0", "eatChar", "sleepAnim_0" };
        var renderers = Object.FindObjectsByType<SpriteRenderer>(FindObjectsInactive.Include).Where(renderer => names.Contains(renderer.name)).ToArray();
        var buttons = Object.FindObjectsByType<Button>(FindObjectsInactive.Include).Where(button => Enumerable.Range(0, button.onClick.GetPersistentEventCount()).Any(index => button.onClick.GetPersistentTarget(index) == switcher)).ToArray();
        Require(buttons.Length == 6, "Missing state buttons");
        foreach (var button in buttons)
        {
            button.onClick.Invoke();
            var active = renderers.Where(renderer => renderer.gameObject.activeSelf).ToArray();
            Require(active.Length == 1, "State activated more than one character");
            ValidateFurniture(Find<CharacterView>(), renderers, active[0].name);
            var animator = active[0].GetComponent<Animator>();
            animator.Rebind();
            animator.Update(0);
            var first = active[0].sprite;
            animator.Update(0.26f);
            Require(active[0].sprite != first, "Character animation is not advancing");
        }
        machine.ChangeState<IdleState>();
        Require(renderers.All(renderer => !renderer.gameObject.activeSelf), "Idle did not hide animations");
        ValidateFurniture(Find<CharacterView>(), renderers, null);
        switcher.ChangeStateOnSleep();
        Debug.Log("VALIDATED: resumed work progress and all six animation buttons");
    }

    private static void ValidateFurniture(CharacterView view, SpriteRenderer[] characters, string activeName)
    {
        foreach (var entry in new[] {
            new[] { "workFurniture", "workAnim_0" },
            new[] { "gamingFurniture", "playAnim_0" },
            new[] { "sleepFurniture", "sleepAnim_0" }
        })
        {
            var furniture = Get<GameObject>(view, entry[0]);
            Require(furniture.activeSelf == (activeName != entry[1]), "Furniture visibility does not match animation: " + entry[0]);
            if (activeName != entry[1])
                continue;

            var character = characters.Single(renderer => renderer.name == activeName);
            var original = furniture.GetComponent<SpriteRenderer>();
            Require(Vector3.Distance(character.transform.position, furniture.transform.position) < 0.00001f, "Animation shifted furniture position");
            Require(Vector3.Distance(character.transform.lossyScale, furniture.transform.lossyScale) < 0.00001f, "Animation changed furniture scale");
            Require(Quaternion.Angle(character.transform.rotation, furniture.transform.rotation) < 0.001f, "Animation changed furniture rotation");
            Require(character.sortingLayerID == original.sortingLayerID && character.sortingOrder == original.sortingOrder, "Animation changed furniture sorting");

            var clip = character.GetComponent<Animator>().runtimeAnimatorController.animationClips.Single();
            var binding = AnimationUtility.GetObjectReferenceCurveBindings(clip).Single();
            foreach (var key in AnimationUtility.GetObjectReferenceCurve(clip, binding))
            {
                var sprite = (Sprite)key.value;
                Vector2 expectedPivot;
                if (activeName == "workAnim_0")
                    expectedPivot = new Vector2(51, 42);
                else if (activeName == "sleepAnim_0")
                    expectedPivot = new Vector2(52.5f, 33.5f);
                else
                    expectedPivot = new Vector2(119.5f, sprite.name == "playAnim_5" ? 74 : 73);

                Require(Vector2.Distance(sprite.pivot, expectedPivot) < 0.001f, "Frame pivot moved furniture: " + sprite.name);
                Require(sprite.pixelsPerUnit == original.sprite.pixelsPerUnit, "Frame changed pixel scale");
            }
        }
    }

    private static void ValidateView(MonoBehaviour view, string field, Action missedUpdate, Action nextUpdate)
    {
        view.enabled = false;
        missedUpdate();
        view.enabled = true;
        Require(Get<TMP_Text>(view, field).text == "77", "View did not refresh on enable");
        nextUpdate();
        Require(Get<TMP_Text>(view, field).text == "88", "View lost event subscription");
        Debug.Log("VALIDATED: subscriptions for " + view.GetType().Name);
    }

    private static void ValidateCasino()
    {
        var controller = Find<CasinoController>();
        var wallet = Get<CasinoWallet>(controller, "casinoWallet");
        Require(wallet.Balance == 100, "Casino did not restore balance");
        controller.SpinClick();
        Require(wallet.Balance == 100, "Casino spun before selecting a color");
        ValidateView(Find<CasinoWalletView>(), "balanceView", () => GameEventBus.RaiseUpdateCasinoBalance(77), () => GameEventBus.RaiseUpdateCasinoBalance(88));
        ValidateView(Find<BettingView>(), "betValue", () => GameEventBus.RaiseUpdateBetValueChange(77), () => GameEventBus.RaiseUpdateBetValueChange(88));
        controller.BetColorClick(2);
        controller.SpinClick();
        Require(wallet.Balance == 90 || wallet.Balance == 110, "Unexpected casino payout");
        SessionState.SetInt("CasinoBalance", wallet.Balance);
        var saved = PlayerDataSave.Load<SaveData>();
        Require(saved.DayNumber == 7 && saved.HealthStat > 98, "Casino overwrote other save fields");
        Debug.Log("VALIDATED: casino startup, payout and save preservation");
    }

    private static void ValidateThoughts()
    {
        var stats = Find<StatsController>();
        var thoughts = Find<ThoughtController>();
        foreach (var entry in new[] { new[] { "FoodStat", "hasShownHungryThought" }, new[] { "EnergyStat", "hasShownEnergyThought" }, new[] { "MentalStat", "hasShownMentalThought" }, new[] { "HealthStat", "hasShownHealthThought" } })
        {
            var data = new SaveData();
            stats.SetSavedStats(data);
            Invoke(stats, "SetMultipliers");
            typeof(SaveData).GetField(entry[0]).SetValue(data, 10f);
            stats.SetSavedStats(data);
            Invoke(stats, "SetMultipliers");
            Require(Get<bool>(thoughts, entry[1]), "Deficit thought was not shown");
            stats.SetSavedStats(new SaveData());
            Invoke(stats, "SetMultipliers");
            Require(!Get<bool>(thoughts, entry[1]), "Recovery did not reset thought flag");
            stats.SetSavedStats(data);
            Invoke(stats, "SetMultipliers");
            Require(Get<bool>(thoughts, entry[1]), "Repeated deficit did not show thought");
        }
        stats.SetSavedStats(new SaveData());
        Debug.Log("VALIDATED: recovery and repeated warnings for all four stats");
    }

    private static void ValidateEventChoices()
    {
        var stats = Find<StatsController>();
        var controller = Find<EventController>();
        var simple = ScriptableObject.CreateInstance<SimpleEvent>();
        simple.foodChangeValue = -1;
        simple.energyChangeValue = -2;
        simple.mentalHealthChangeValue = -3;
        simple.healthChangeValue = -4;
        stats.SetSavedStats(new SaveData());
        Set(controller, "currentEvent", simple);
        controller.OnSimpleEventButtonClicked();
        Require(stats.FoodStat == 99 && stats.EnergyStat == 98 && stats.MentalStat == 97 && stats.HealthStat == 96, "Manual event applied incorrect stats");
        stats.SetSavedStats(new SaveData());
        Set(controller, "currentEvent", simple);
        var routine = (IEnumerator)Invoke(controller, "AutoHideCoroutine");
        routine.MoveNext();
        routine.MoveNext();
        Require(stats.FoodStat == 99 && stats.EnergyStat == 98 && stats.MentalStat == 97 && stats.HealthStat == 96, "Automatic event differs from manual event");
        Object.Destroy(simple);
        stats.SetSavedStats(new SaveData());
        Debug.Log("VALIDATED: manual and automatic event changes");
    }

    private static void TriggerLoss(bool mental, int nextStage)
    {
        var stats = Find<StatsController>();
        StopSimulation();
        stats.SetSavedStats(new SaveData { HealthStat = mental ? 100 : 0, MentalStat = mental ? 0 : 100 });
        SessionState.SetInt(Stage, nextStage);
        frames = 0;
        Invoke(stats, "Update");
        Invoke(stats, "Update");
        Require(stats.IsGameOver, "Loss was not latched");
    }

    private static void ValidateFinal(bool mental, int expectedLosses)
    {
        Require(SceneManager.GetActiveScene().name == "FinalScene", "Loss did not open FinalScene");
        Require(SessionState.GetInt("LossCount", 0) == expectedLosses, "Loss event fired more than once");
        var view = Find<FinalView>();
        Require(Get<GameObject>(view, "suicideCharacter").activeSelf == mental, "Incorrect mental loss animation");
        Require(Get<GameObject>(view, "exhaustionCharacter").activeSelf != mental, "Incorrect health loss animation");
        Debug.Log("VALIDATED: one-shot loss and final animation " + GameController.LossType);
    }

    private static void CountLoss(LossGameTypes lossType) => SessionState.SetInt("LossCount", SessionState.GetInt("LossCount", 0) + 1);
    private static T Find<T>() where T : Object => Object.FindObjectsByType<T>(FindObjectsInactive.Include).Single();
    private static T Get<T>(object target, string field) => (T)target.GetType().GetField(field, BindingFlags.Instance | BindingFlags.NonPublic).GetValue(target);
    private static void Set(object target, string field, object value) => target.GetType().GetField(field, BindingFlags.Instance | BindingFlags.NonPublic).SetValue(target, value);
    private static object Invoke(object target, string method) => target.GetType().GetMethod(method, BindingFlags.Instance | BindingFlags.NonPublic).Invoke(target, null);
    private static void Require(bool condition, string message) { if (!condition) throw new Exception(message); }
    private static void Next(int stage, string scene) { SessionState.SetInt(Stage, stage); frames = 0; SceneManager.LoadScene(scene); }
}
