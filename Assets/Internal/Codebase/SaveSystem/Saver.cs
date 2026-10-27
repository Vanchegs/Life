using UnityEngine;

namespace Codebase
{
    public class Saver : MonoBehaviour
    {
        [SerializeField] private StatsController statsController;
        [SerializeField] private WalletController walletController;
        [SerializeField] private DaysCounter daysCounter;

        private SaveData saveData;
        private bool isInitialized;

        private void OnEnable() =>
            GameEventBus.OnSaveGame += Save;

        private void OnDisable() =>
            GameEventBus.OnSaveGame -= Save;

        private void Start() =>
            Initialize();

        private void OnApplicationPause(bool isPaused)
        {
            if (isPaused)
                Save();
        }

        private void OnApplicationQuit() =>
            Save();

        private void OnDestroy() =>
            Save();

        private void Save()
        {
            if (!isInitialized || statsController == null || walletController == null || daysCounter == null)
                return;

            saveData.Balance = walletController.GetWallet().GetBalance();
            saveData.DayNumber = daysCounter.DayNumber;
            saveData.FoodStat = statsController.FoodStat;
            saveData.EnergyStat = statsController.EnergyStat;
            saveData.MentalStat = statsController.MentalStat;
            saveData.HealthStat = statsController.HealthStat;

            if (PlayerDataSave.Save(saveData))
                Debug.Log("Game saved successfully");
        }

        private void Initialize()
        {
            if (isInitialized)
                return;

            saveData = PlayerDataSave.Load<SaveData>() ?? new SaveData(statsController.MaxValue);

            walletController.GetWallet().SetSaveBalance(saveData.Balance);
            statsController.SetSavedStats(saveData);
            daysCounter.SetSavedDay(saveData.DayNumber);

            isInitialized = true;
        }
    }
}
