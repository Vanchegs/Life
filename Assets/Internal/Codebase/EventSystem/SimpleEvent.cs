using UnityEngine;

namespace Codebase
{
    [CreateAssetMenu(menuName = "Configs/Simple Event Config", fileName = "SimpleEventConfig")]
    public class SimpleEvent : GameEvent
    {
        public string buttonText;
        
        [Space(10)]
        [Header("Second solution change value")]
        public int foodChangeValue;
        public int energyChangeValue;
        public int healthChangeValue;
        public int mentalHealthChangeValue;
        public int moneyChangeValue;
    }
}