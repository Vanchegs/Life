using UnityEngine;
// ReSharper disable InconsistentNaming

namespace Codebase
{
    [CreateAssetMenu(menuName = "Configs/Simple Event Config", fileName = "SimpleEventConfig")]
    public class SimpleEvent : GameEvent
    {
        public string ButtonText;
        
        [Space(10)]
        [Header("Second solution change value")]
        public int secondSolutionFoodChangeValue;
        public int secondSolutionEnergyValue;
        public int secondSolutionHealthChangeValue;
        public int secondSolutionMentalHealthChangeValue;
        public int secondSolutionMoneyChangeValue;
    }
}