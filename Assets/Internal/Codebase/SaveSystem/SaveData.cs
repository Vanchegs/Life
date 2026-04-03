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
    }
}