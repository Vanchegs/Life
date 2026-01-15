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
            if (currentValue >= MinValue) 
                currentValue -= decreaseValue * Time.deltaTime;
        }

        public void IncreaseValue()
        {
            if (currentValue < maxValue)
            {
                currentValue += increaseValue * Time.deltaTime;
                Debug.Log(currentValue);
            }
            else
                Debug.Log("Полный запас стата");
        }

        public void EventStatChange(int changeValue)
        {
            currentValue = Math.Clamp(currentValue + changeValue, MinValue, maxValue);
            Debug.Log(currentValue);
        }

        public float GetCurrentStat() => 
            currentValue;
    }
}

