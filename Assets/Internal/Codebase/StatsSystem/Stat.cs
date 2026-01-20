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
        private float multiplier;

        public Stat(StatType statType, int maxValue, float decreaseValue, float increaseValue)
        {
            this.statType = statType;
            
            this.maxValue = maxValue;
            this.decreaseValue = decreaseValue;
            this.increaseValue = increaseValue;

            currentValue = maxValue;
            ChangeMultiplier(1);
        }

        public StatType GetStatType() => 
            statType;

        public void DecreaseValue()
        { 
            currentValue = Mathf.Clamp(currentValue - decreaseValue * Time.deltaTime * multiplier, MinValue, maxValue);
        }

        public void ChangeMultiplier(float _multiplier)
        {
            if (_multiplier > 3)
                return;
            multiplier = _multiplier;
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

