using System;

namespace Codebase
{
    [Serializable]
    public class Stat
    {
        private int maxValue;
        private int minValue;

        private float currentValue;

        public Stat(int maxValue, int minValue)
        {
            this.maxValue = maxValue;
            this.minValue = minValue;
        }

        public void InitializeStat() => 
            currentValue = maxValue;

        public float GetCurrentStat() => 
            currentValue;
    }
}

