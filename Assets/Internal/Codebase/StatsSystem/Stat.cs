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
            if (decreaseValue < maxValue)
            {
                currentValue += increaseValue * Time.deltaTime;
                Debug.Log(currentValue);
            }
            else
                Debug.Log("Полный запас стата");
        }

        public float GetCurrentStat() => 
            currentValue;
    }
}

