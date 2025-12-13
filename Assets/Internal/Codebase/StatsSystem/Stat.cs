using UnityEngine;

namespace Codebase
{
    public class Stat
    {
        private const int MinValue = 0;
        
        private StatType statType;
        
        private int maxValue;

        private float currentValue;
        private float decreaseValue;

        public Stat(StatType statType, int maxValue, float decreaseValue)
        {
            this.statType = statType;
            
            this.maxValue = maxValue;
            this.decreaseValue = decreaseValue;
            
            currentValue = maxValue;
        }

        public StatType GetStatType() => 
            statType;

        public void DecreaseValue() => 
            currentValue -= decreaseValue * Time.deltaTime;

        public float GetCurrentStat() => 
            currentValue;
    }
}

