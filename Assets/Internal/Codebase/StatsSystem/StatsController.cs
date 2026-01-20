using System.Collections.Generic;
using UnityEngine;

namespace Codebase
{
    public class StatsController : MonoBehaviour
    {
        private const float NormalMultiplier = 1;
        private const float DeficitMultiplier = 5f;
        private const int DeficitValue = 15;
        
        [SerializeField] private StatConfig statConfig;
        [SerializeField] private StatVisual statVisual;
        
        private Stat foodStat;
        private Stat energyStat;
        private Stat mentalHealthStat;
        private Stat healthStat;

        private List<Stat> stats;

        private void Start()
        {
            InitializeStats();
            statVisual.Init(statConfig, foodStat, energyStat, healthStat, mentalHealthStat);
        }

        private void Update()
        {
            UpdateStats();
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

        public void ChangeStatsAfterEvent(int foodChangeValue, int energyChangeValue, int mentalHealthChangeValue, int healthChangeValue)
        {
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

        private void SetMultipliers()
        {
            float energyMultiplier = NormalMultiplier;
            float healthMultiplier = NormalMultiplier; 
            float mentalMultiplier = NormalMultiplier;

            if (foodStat.GetCurrentStat() < DeficitValue)
            {
                energyMultiplier = DeficitMultiplier;
                healthMultiplier = DeficitMultiplier;
                mentalMultiplier = DeficitMultiplier;
            }

            if (energyStat.GetCurrentStat() < DeficitValue)
            {
                healthMultiplier = DeficitMultiplier;
                mentalMultiplier = DeficitMultiplier;
            }

            if (mentalHealthStat.GetCurrentStat() < DeficitValue)
            {
                energyMultiplier = DeficitMultiplier;
                healthMultiplier = DeficitMultiplier;
            }

            if (healthStat.GetCurrentStat() < DeficitValue)
            {
                energyMultiplier = DeficitMultiplier;
                mentalMultiplier = DeficitMultiplier;
            }

            energyStat.ChangeMultiplier(energyMultiplier);
            healthStat.ChangeMultiplier(healthMultiplier);
            mentalHealthStat.ChangeMultiplier(mentalMultiplier);
        }
    }
}