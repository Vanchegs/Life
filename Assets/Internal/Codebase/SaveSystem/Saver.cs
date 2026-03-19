using UnityEngine;

namespace Codebase
{
    public class Saver : MonoBehaviour
    {
        [SerializeField] private StatsController statsController;
        [SerializeField] private WalletController walletController;
        
        private SaveData saveData;
        private bool isInitialized = false;

        private void OnEnable() => 
            GameEventBus.SaveGame += Save;

        private void OnDisable() => 
            GameEventBus.SaveGame -= Save;

        private void Awake() => 
            Initialize();

        public void Save()
        {
            if (!isInitialized)
            {
                Debug.LogWarning("Attempted to save before initialization!");
                return;
            }

            UpdateSaveData();
            
            try
            {
                PlayerDataSave.Save(saveData);
                Debug.Log("Game saved successfully");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Failed to save game: {e.Message}");
            }
        }

        private void Initialize()
        {
            if (isInitialized) return;

            if (statsController == null)
            {
                Debug.LogError("StatsController reference is missing in Saver!");
                return;
            }

            if (walletController == null)
            {
                Debug.LogError("WalletController reference is missing in Saver!");
                return;
            }

            LoadData();
            ApplyLoadedData();
            
            isInitialized = true;
        }

        private void LoadData()
        {
            try
            {
                saveData = PlayerDataSave.Load<SaveData>();
                if (saveData == null)
                {
                    Debug.Log("No save data found, creating new...");
                    saveData = new SaveData();
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Failed to load save data: {e.Message}");
                saveData = new SaveData();
            }
        }

        private void ApplyLoadedData()
        {
            /*// Применяем загруженные данные к контроллерам
            walletController.SetBalance(saveData.Balance);
            
            statsController.SetStats(
                saveData.FoodStat,
                saveData.EnergyStat,
                saveData.MentalStat,
                saveData.HealthStat
            );
            */
            
            Debug.Log("Loaded data applied to game");
        }

        private void UpdateSaveData()
        {
            /*saveData.Balance = walletController.GetBalance();
            saveData.FoodStat = statsController.GetFoodStat();
            saveData.EnergyStat = statsController.GetEnergyStat();
            saveData.MentalStat = statsController.GetMentalStat();
            saveData.HealthStat = statsController.GetHealthStat();*/
            
            Debug.Log($"Saving stats: Food={saveData.FoodStat}, Energy={saveData.EnergyStat}, " +
                     $"Mental={saveData.MentalStat}, Health={saveData.HealthStat}, Balance={saveData.Balance}");
        }
    }
}