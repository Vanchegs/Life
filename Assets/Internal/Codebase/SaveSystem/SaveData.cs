using System;
using System.Diagnostics.CodeAnalysis;

namespace Codebase
{
    [Serializable]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class SaveData
    {
        public int Balance;
        public int DayNumber;
        public float FoodStat;
        public float MentalStat;
        public float HealthStat;
        public float EnergyStat;

        public SaveData() : this(100) { }

        public SaveData(int maxValue)
        {
            DayNumber = 1;
            FoodStat = maxValue;
            MentalStat = maxValue;
            HealthStat = maxValue;
            EnergyStat = maxValue;
        }
    }
}
