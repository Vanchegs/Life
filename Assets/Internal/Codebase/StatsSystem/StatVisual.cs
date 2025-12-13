using UnityEngine;
using UnityEngine.UI;

namespace Codebase
{
    public class StatVisual : MonoBehaviour
    {
        [SerializeField] private Slider foodSlider;
        [SerializeField] private Slider sleepSlider;
        [SerializeField] private Slider healthSlider;
        [SerializeField] private Slider mentalHealthSlider;

        private Stat foodStat;
        private Stat sleepStat;
        private Stat healthStat;
        private Stat mentalHealthStat;

        public void Init(StatConfig statConfig, Stat foodStat, Stat sleepStat, Stat healthStat, Stat mentalHealthStat)
        {
            InitSlider(foodSlider, statConfig);
            InitSlider(sleepSlider, statConfig);
            InitSlider(healthSlider, statConfig);
            InitSlider(mentalHealthSlider, statConfig);
        
            this.foodStat = foodStat;
            this.sleepStat = sleepStat;
            this.healthStat = healthStat;
            this.mentalHealthStat = mentalHealthStat;
        
            UpdateValue();
        }
        
        private void InitSlider(Slider slider, StatConfig config)
        {
            if (slider == null) 
            {
                Debug.LogError($"Slider is null in {gameObject.name}");
                return;
            }
        
            slider.minValue = config.minValue;
            slider.maxValue = config.maxValue;
            slider.value = config.maxValue; // Начальное значение = максимум
        }
    
        public void UpdateValue()
        {
            foodSlider.value = Mathf.Clamp(foodStat.GetCurrentStat(), foodSlider.minValue, foodSlider.maxValue);
            sleepSlider.value = Mathf.Clamp(sleepStat.GetCurrentStat(), sleepSlider.minValue, sleepSlider.maxValue);
            healthSlider.value = Mathf.Clamp(healthStat.GetCurrentStat(), healthSlider.minValue, healthSlider.maxValue);
            mentalHealthSlider.value = Mathf.Clamp(mentalHealthStat.GetCurrentStat(), mentalHealthSlider.minValue, mentalHealthSlider.maxValue);
        }
    }
}