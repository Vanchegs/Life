using System.Collections.Generic;
using UnityEngine;

namespace Codebase
{
    public class StatsController : MonoBehaviour
    {
        private const float NormalMultiplier = 1;
        private const float DeficitMultiplier = 2;
        private const int DeficitValue = 20;
        
        public float FoodStat => foodStat.GetCurrentStat();
        public float EnergyStat => energyStat.GetCurrentStat();
        public float MentalStat => mentalHealthStat.GetCurrentStat();
        public float HealthStat => healthStat.GetCurrentStat();
        public int MaxValue => statConfig.maxValue;
        public bool IsGameOver => isGameOver;
        
        [SerializeField] private StatConfig statConfig;
        [SerializeField] private StatVisual statVisual;
        [SerializeField] private WalletController walletController;
        
        private Stat foodStat;
        private Stat energyStat;
        private Stat mentalHealthStat;
        private Stat healthStat;

        private List<Stat> stats;

        private bool isGameOver;
        private bool isFoodLow;
        private bool isEnergyLow;
        private bool isMentalLow;
        private bool isHealthLow;

        private void Awake() =>
            InitializeStats();

        private void Start()
        {
            statVisual.Init(foodStat, energyStat, healthStat, mentalHealthStat);
        }

        private void Update()
        {
            if (isGameOver)
                return;

            UpdateStats();

            if (CheckLoss())
                return;

            statVisual.UpdateValue();
        }

        private void InitializeStats()
        {
            foodStat = new Stat(StatType.FoodStat, statConfig.maxValue, statConfig.foodDecreaseValue, statConfig.foodIncreaseValue);
            energyStat = new Stat(StatType.EnergyStat, statConfig.maxValue, statConfig.energyDecreaseValue, statConfig.energyIncreaseValue);
            mentalHealthStat = new Stat(StatType.MentalHealthStat, statConfig.maxValue, statConfig.mentalDecreaseValue, statConfig.mentalIncreaseValue);
            healthStat = new Stat(StatType.HealthStat, statConfig.maxValue, statConfig.healthDecreaseValue, statConfig.healthIncreaseValue);
            
            stats = new List<Stat> { foodStat, energyStat, mentalHealthStat, healthStat };
        }

        public void IncreaseCurrentStat(StatType statType)
        {
            if (isGameOver)
                return;

            switch (statType)
            {
                case StatType.FoodStat:
                    foodStat.IncreaseValue();
                    break;
                case StatType.HealthStat:
                    healthStat.IncreaseValue();
                    break;
                case StatType.EnergyStat:
                    energyStat.IncreaseValue();
                    break;
                case StatType.MentalHealthStat:
                    mentalHealthStat.IncreaseValue();
                    break;
                default:
                    return;
            }
        }
        
        public void ChangeStats(int moneyChangeValue, int foodChangeValue, int energyChangeValue, int mentalHealthChangeValue, int healthChangeValue)
        {
            if (isGameOver)
                return;

            walletController.GetWallet()?.ChangeBalance(moneyChangeValue);
            
            foodStat.EventStatChange(foodChangeValue);
            energyStat.EventStatChange(energyChangeValue);
            mentalHealthStat.EventStatChange(mentalHealthChangeValue);
            healthStat.EventStatChange(healthChangeValue);
        }

        private void UpdateStats()
        {
            SetMultipliers();

            foreach (var stat in stats)
            {
                stat.DecreaseValue();
            }
        }

        public void SetSavedStats(SaveData saveData)
        {
            foodStat.SetSavedValue(saveData.FoodStat);
            energyStat.SetSavedValue(saveData.EnergyStat);
            healthStat.SetSavedValue(saveData.HealthStat);
            mentalHealthStat.SetSavedValue(saveData.MentalStat);
        }

        private void SetMultipliers()
        {
            var energyMultiplier = NormalMultiplier;
            var healthMultiplier = NormalMultiplier; 
            var mentalMultiplier = NormalMultiplier;

            if (CheckDeficit(foodStat, ref isFoodLow))
            {
                energyMultiplier = DeficitMultiplier;
                healthMultiplier = DeficitMultiplier;
                mentalMultiplier = DeficitMultiplier;
            }

            if (CheckDeficit(energyStat, ref isEnergyLow))
            {
                healthMultiplier = DeficitMultiplier;
                mentalMultiplier = DeficitMultiplier;
            }

            if (CheckDeficit(mentalHealthStat, ref isMentalLow))
            {
                energyMultiplier = DeficitMultiplier;
                healthMultiplier = DeficitMultiplier;
            }

            if (CheckDeficit(healthStat, ref isHealthLow))
            {
                energyMultiplier = DeficitMultiplier;
                mentalMultiplier = DeficitMultiplier;
            }

            energyStat.ChangeMultiplier(energyMultiplier);
            healthStat.ChangeMultiplier(healthMultiplier);
            mentalHealthStat.ChangeMultiplier(mentalMultiplier);
        }

        private bool CheckDeficit(Stat stat, ref bool wasLow)
        {
            bool isLow = stat.GetCurrentStat() < DeficitValue;

            if (wasLow == isLow)
                return isLow;

            wasLow = isLow;

            if (!isLow)
            {
                GameEventBus.RaiseStatRecovered(stat.GetStatType());
                return false;
            }

            switch (stat.GetStatType())
            {
                case StatType.FoodStat:
                    GameEventBus.RaiseFellHungry();
                    break;
                case StatType.EnergyStat:
                    GameEventBus.RaiseFellEnergy();
                    break;
                case StatType.MentalHealthStat:
                    GameEventBus.RaiseFellMental();
                    break;
                case StatType.HealthStat:
                    GameEventBus.RaiseFellHealth();
                    break;
            }

            return true;
        }

        private bool CheckLoss()
        {
            if (HealthStat > 0 && MentalStat > 0)
                return false;

            isGameOver = true;
            GameEventBus.RaiseLossGame(HealthStat <= 0 ? LossGameTypes.Exhaustion : LossGameTypes.Suicide);
            return true;
        }
    }
}
