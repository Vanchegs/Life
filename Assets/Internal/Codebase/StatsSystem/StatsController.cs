using UnityEngine;

namespace Codebase
{
    public class StatsController : MonoBehaviour
    {
        private Stat foodStat;
        private Stat sleepStat;
        private Stat mentalHealthStat;
        private Stat healthStat;

        private StatConfig statConfig;

        private void InitializeStats()
        {
            foodStat = new Stat(statConfig.maxValue, statConfig.foodDecreaseValue);
            sleepStat = new Stat(statConfig.maxValue, statConfig.sleepDecreaseValue);
            mentalHealthStat = new Stat(statConfig.maxValue, statConfig.mentalDecreaseValue);
            healthStat = new Stat(statConfig.maxValue, statConfig.healthDecreaseValue);
        }
    }
}