using UnityEngine;

namespace Codebase
{
    [CreateAssetMenu(menuName = "Configs", fileName = "StatConfig", order = 1)]
    public class StatConfig : ScriptableObject
    {
        public int maxValue;
        public int minValue;
        
        [Header("Food Stat")] 
        public float foodDecreaseValue;
        public float foodIncreaseValue;

        [Header("Sleep Stat")] 
        public float sleepDecreaseValue;
        public float sleepIncreaseValue;

        [Header("Health Stat")] 
        public float healthDecreaseValue;
        public float healthIncreaseValue;

        [Header("Mental Health Stat")] 
        public float mentalDecreaseValue;
        public float mentalIncreaseValue;
    }
}