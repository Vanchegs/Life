using UnityEngine;

namespace Codebase
{
    [CreateAssetMenu(menuName = "Configs", fileName = "StatConfig", order = 1)]
    public class StatConfig : ScriptableObject
    {
        public int maxValue;
        
        [Header("Food Stat")] 
        public float foodDecreaseValue;

        [Header("Sleep Stat")] 
        public float sleepDecreaseValue;

        [Header("Health Stat")] 
        public float healthDecreaseValue;

        [Header("Mental Health Stat")] 
        public float mentalDecreaseValue;
    }
}