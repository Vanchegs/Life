using System;
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

        private void Start() => 
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
            walletController.GetWallet().SetSaveBalance(saveData.Balance);
            
            statsController.SetSavedStats(saveData);
            
            Debug.Log("Loaded data applied to game");
        }

        private void UpdateSaveData()
        {
            saveData.Balance = walletController.GetWallet().GetBalance();
            saveData.FoodStat = statsController.FoodStat;
            saveData.EnergyStat = statsController.EnergyStat;
            saveData.MentalStat = statsController.MentalStat;
            saveData.HealthStat = statsController.HealthStat;
            
            Debug.Log($"Saving stats: Food={saveData.FoodStat}, Energy={saveData.EnergyStat}, " +
                     $"Mental={saveData.MentalStat}, Health={saveData.HealthStat}, Balance={saveData.Balance}");
        }
    }
}