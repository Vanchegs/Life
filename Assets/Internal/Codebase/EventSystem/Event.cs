using UnityEngine;

namespace Codebase
{
    [CreateAssetMenu(menuName = "Configs/Event Config", fileName = "EventConfig")]
    public class Event : ScriptableObject
    {
        [Header("Info")] 
        public string eventName;
        public string description;

        [Header("Solutions"), Space(10)]
        public string firstSolution;
        public string secondSolution;

        [Header("Food Stat")] 
        public int foodChangeValue;
        public int healthChangeValue;
        public int mentalHealthChangeValue;
        public int moneyChangeValue;
    }
}