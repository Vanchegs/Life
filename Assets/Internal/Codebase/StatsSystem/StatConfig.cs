using UnityEngine;

namespace Codebase
{
    [CreateAssetMenu(menuName = "Configs/Stat Config", fileName = "StatConfig")]
    public class StatConfig : ScriptableObject
    {
        public int maxValue;
        public int minValue;
        
        [Header("Food Stat")] 
        public float foodDecreaseValue;
        public float foodIncreaseValue;

        [Header("Sleep Stat")] 
        public float energyDecreaseValue;
        public float energyIncreaseValue;

        [Header("Health Stat")] 
        public float healthDecreaseValue;
        public float healthIncreaseValue;

        [Header("Mental Health Stat")] 
        public float mentalDecreaseValue;
        public float mentalIncreaseValue;
    }
}