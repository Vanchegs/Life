using UnityEngine;
using UnityEngine.UI;

namespace Codebase
{
    public class StatVisual : MonoBehaviour
    {
        [SerializeField] private Slider slider;

        private Stat stat;
        
        public void Init(StatConfig statConfig, Stat stat)
        {
            slider.minValue = statConfig.minValue;
            slider.maxValue = statConfig.maxValue;
            this.stat = stat;
            slider.value = stat.GetCurrentStat();
        }
    
        public void UpdateValue()
        {
            slider.value = Mathf.Clamp(stat.GetCurrentStat(), slider.minValue, slider.maxValue);
        }
    }
}