using System.Collections.Generic;
using UnityEngine;

namespace Codebase
{
    public class StatsController : MonoBehaviour
    {
        private const float NormalMultiplier = 1;
        private const float DeficitMultiplier = 2f;
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
            if (foodStat.GetCurrentStat() < 15)
            {
                energyStat.ChangeMultiplier(DeficitMultiplier);
                healthStat.ChangeMultiplier(DeficitMultiplier);
                mentalHealthStat.ChangeMultiplier(DeficitMultiplier);
            }
            else
            {
                energyStat.ChangeMultiplier(NormalMultiplier);
                healthStat.ChangeMultiplier(NormalMultiplier);
                mentalHealthStat.ChangeMultiplier(NormalMultiplier);
            }

            if (energyStat.GetCurrentStat() < 15)
            {
                healthStat.ChangeMultiplier(DeficitMultiplier);
                mentalHealthStat.ChangeMultiplier(DeficitMultiplier);
            }
            else
            {
                healthStat.ChangeMultiplier(NormalMultiplier);
                mentalHealthStat.ChangeMultiplier(NormalMultiplier);
            }

            if (mentalHealthStat.GetCurrentStat() < 15)
            {
                energyStat.ChangeMultiplier(DeficitMultiplier); 
                healthStat.ChangeMultiplier(DeficitMultiplier);
            }
            else
            {
                energyStat.ChangeMultiplier(NormalMultiplier);
                healthStat.ChangeMultiplier(NormalMultiplier);
            }
            
            foreach (var stat in stats)
            {
                stat.DecreaseValue();
            }
        }
    }
}