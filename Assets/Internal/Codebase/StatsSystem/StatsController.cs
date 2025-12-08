using System.Collections.Generic;
using UnityEngine;

namespace Codebase
{
    public class StatsController : MonoBehaviour
    {
        [SerializeField] private StatConfig statConfig;
        [SerializeField] private StatVisual statVisual;
        
        private Stat foodStat;
        private Stat sleepStat;
        private Stat mentalHealthStat;
        private Stat healthStat;

        private List<Stat> stats;

        private void Start()
        {
            InitializeStats();
            statVisual.Init(statConfig.minValue, statConfig.maxValue, foodStat);
        }

        private void Update()
        {
            UpdateStats();
            statVisual.UpdateValue();
        }

        private void InitializeStats()
        {
            foodStat = new Stat(statConfig.maxValue, statConfig.foodDecreaseValue);
            sleepStat = new Stat(statConfig.maxValue, statConfig.sleepDecreaseValue);
            mentalHealthStat = new Stat(statConfig.maxValue, statConfig.mentalDecreaseValue);
            healthStat = new Stat(statConfig.maxValue, statConfig.healthDecreaseValue);
            
            stats = new List<Stat> { foodStat, sleepStat, mentalHealthStat, healthStat };
        }

        private void UpdateStats()
        {
            foreach (var stat in stats)
            {
                stat.DecreaseValue();
                Debug.Log(stat.GetCurrentStat());
            }
        }
    }
}