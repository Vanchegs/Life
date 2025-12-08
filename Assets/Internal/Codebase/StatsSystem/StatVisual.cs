using UnityEngine;
using UnityEngine.UI;

namespace Codebase
{
    public class StatVisual : MonoBehaviour
    {
        [SerializeField] private Slider slider;

        private Stat stat;
        
        public void Init(float min, float max, Stat stat)
        {
            slider.minValue = min;
            slider.maxValue = max;
            this.stat = stat;
            slider.value = stat.GetCurrentStat();
        }
    
        public void UpdateValue()
        {
            slider.value = Mathf.Clamp(stat.GetCurrentStat(), slider.minValue, slider.maxValue);
        }
    }
}