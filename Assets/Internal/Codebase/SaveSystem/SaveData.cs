using System;
using System.Diagnostics.CodeAnalysis;

namespace Codebase
{
    [Serializable]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class SaveData
    {
        public int Balance;
        public int FoodStat;
        public int MentalStat;
        public int HealthStat;
        public int EnergyStat;
    }
}