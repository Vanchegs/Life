using System;
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
        private float increaseValue;

        public Stat(StatType statType, int maxValue, float decreaseValue, float increaseValue)
        {
            this.statType = statType;
            
            this.maxValue = maxValue;
            this.decreaseValue = decreaseValue;
            this.increaseValue = increaseValue;

            currentValue = maxValue;
        }

        public StatType GetStatType() => 
            statType;

        public void DecreaseValue()
        {
            currentValue = Mathf.Clamp(currentValue - decreaseValue * Time.deltaTime, MinValue, maxValue);
        }

        public void IncreaseValue()
        {
            if (currentValue < maxValue)
                currentValue = Mathf.Clamp(currentValue + increaseValue * Time.deltaTime, MinValue, maxValue);
        }

        public void EventStatChange(float changeValue)
        {
            currentValue = Mathf.Clamp(currentValue + changeValue, MinValue, maxValue);
            Debug.Log($"Stat {statType} changed to: {currentValue}");
        }

        public float GetCurrentStat() => 
            currentValue;
    }
}

