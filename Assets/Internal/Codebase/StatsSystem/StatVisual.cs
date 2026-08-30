using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase
{
    public class StatVisual : MonoBehaviour
    {
        [SerializeField] private Slider foodSlider;
        [SerializeField] private Slider sleepSlider;
        [SerializeField] private Slider healthSlider;
        [SerializeField] private Slider mentalSlider;

        [SerializeField] private SpriteRenderer energyStatusSprite;
        [SerializeField] private SpriteRenderer foodStatusSprite;
        [SerializeField] private SpriteRenderer mentalStatusSprite;
        [SerializeField] private SpriteRenderer healthStatusSprite;

        [SerializeField] private List<Sprite> energyStatusSprites;
        [SerializeField] private List<Sprite> foodStatusSprites;
        [SerializeField] private List<Sprite> mentalStatusSprites;
        [SerializeField] private List<Sprite> healthStatusSprites;

        private Stat foodStat;
        private Stat energyStat;
        private Stat healthStat;
        private Stat mentalHealthStat;

        public void Init(StatConfig statConfig, Stat foodStat, Stat energyStat, Stat healthStat, Stat mentalHealthStat)
        {
            InitSlider(foodSlider, statConfig);
            InitSlider(sleepSlider, statConfig);
            InitSlider(healthSlider, statConfig);
            InitSlider(mentalSlider, statConfig);
        
            this.foodStat = foodStat;
            this.energyStat = energyStat;
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
            slider.value = config.maxValue;
        }
    
        public void UpdateValue()
        {
            foodSlider.value = Mathf.Clamp(foodStat.GetCurrentStat(), foodSlider.minValue, foodSlider.maxValue);
            sleepSlider.value = Mathf.Clamp(energyStat.GetCurrentStat(), sleepSlider.minValue, sleepSlider.maxValue);
            healthSlider.value = Mathf.Clamp(healthStat.GetCurrentStat(), healthSlider.minValue, healthSlider.maxValue);
            mentalSlider.value = Mathf.Clamp(mentalHealthStat.GetCurrentStat(), mentalSlider.minValue, mentalSlider.maxValue);

            if (foodStat.GetCurrentStat() >= 66)
                foodStatusSprite.sprite = foodStatusSprites[0];
            else if (foodStat.GetCurrentStat() >= 33 && foodStat.GetCurrentStat() < 66)
                foodStatusSprite.sprite = foodStatusSprites[1];
            else
                foodStatusSprite.sprite = foodStatusSprites[2];
            
            if (energyStat.GetCurrentStat() >= 66)
                energyStatusSprite.sprite = energyStatusSprites[0];
            else if (energyStat.GetCurrentStat() >= 33 && energyStat.GetCurrentStat() < 66)
                energyStatusSprite.sprite = energyStatusSprites[1];
            else
                energyStatusSprite.sprite = energyStatusSprites[2];

            if (mentalHealthStat.GetCurrentStat() >= 66)
                mentalStatusSprite.sprite = mentalStatusSprites[0];
            else if (mentalHealthStat.GetCurrentStat() >= 33 && mentalHealthStat.GetCurrentStat() < 66)
                mentalStatusSprite.sprite = mentalStatusSprites[1];
            else
                mentalStatusSprite.sprite = mentalStatusSprites[2];

            if (healthStat.GetCurrentStat() >= 66)
                healthStatusSprite.sprite = healthStatusSprites[0];
            else if (healthStat.GetCurrentStat() >= 33 && healthStat.GetCurrentStat() < 66)
                healthStatusSprite.sprite = healthStatusSprites[1];
            else
                healthStatusSprite.sprite = healthStatusSprites[2];
        }
    }
}