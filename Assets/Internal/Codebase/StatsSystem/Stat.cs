using UnityEngine;

namespace Codebase
{
    public class Stat
    {
        private const int MinValue = 0;
        
        private int maxValue;

        private float currentValue;
        private float decreaseValue;

        public Stat(int maxValue, int minValue, float decreaseValue)
        {
            this.maxValue = maxValue;
            this.decreaseValue = decreaseValue;
        }

        public void InitializeStat() => 
            currentValue = maxValue;

        public void DecreaseValue() => 
            currentValue -= decreaseValue * Time.deltaTime;

        public float GetCurrentStat() => 
            currentValue;
    }
}

