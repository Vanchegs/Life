using UnityEngine;

namespace Codebase
{
    [CreateAssetMenu(menuName = "Configs/Event Config", fileName = "EventConfig")]
    public class Event : ScriptableObject
    {
        [Header("Info")] 
        public string eventName;
        public string description;
        
        [Space(10)]
        [Header("Solutions")]
        public string firstSolution;
        public string secondSolution;

        [Space(10)]
        [Header("First solution change value")] 
        public int firstSolutionFoodChangeValue;
        public int firstSolutionEnergyValue;
        public int firstSolutionHealthChangeValue;
        public int firstSolutionMentalHealthChangeValue;
        public int firstSolutionMoneyChangeValue;
        
        [Space(10)]
        [Header("Second solution change value")]
        public int secondSolutionFoodChangeValue;
        public int secondSolutionEnergyValue;
        public int secondSolutionHealthChangeValue;
        public int secondSolutionMentalHealthChangeValue;
        public int secondSolutionMoneyChangeValue;
    }
}