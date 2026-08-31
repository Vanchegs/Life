using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase
{
    public class StatVisual : MonoBehaviour
    {
        [SerializeField] private Image energyStatusImage;
        [SerializeField] private Image foodStatusImage;
        [SerializeField] private Image mentalStatusImage;
        [SerializeField] private Image healthStatusImage;

        [SerializeField] private List<Sprite> energyStatusImages;
        [SerializeField] private List<Sprite> foodStatusImages;
        [SerializeField] private List<Sprite> mentalStatusImages;
        [SerializeField] private List<Sprite> healthStatusImages;

        private Stat foodStat;
        private Stat energyStat;
        private Stat healthStat;
        private Stat mentalHealthStat;
        
        private int lastFoodIndex = -1;
        private int lastEnergyIndex = -1; 
        private int lastMentalIndex = -1; 
        private int lastHealthIndex = -1;

        public void Init(Stat foodStat, Stat energyStat, Stat healthStat, Stat mentalHealthStat)
        {
            this.foodStat = foodStat;
            this.energyStat = energyStat;
            this.healthStat = healthStat;
            this.mentalHealthStat = mentalHealthStat;
        
            UpdateValue();
        }

        public void UpdateValue()
        {
            UpdateStatusSprite(foodStatusImage, foodStatusImages, foodStat.GetCurrentStat(), ref lastFoodIndex);
            UpdateStatusSprite(energyStatusImage, energyStatusImages, energyStat.GetCurrentStat(), ref lastEnergyIndex);
            UpdateStatusSprite(mentalStatusImage, mentalStatusImages, mentalHealthStat.GetCurrentStat(), ref lastMentalIndex);
            UpdateStatusSprite(healthStatusImage, healthStatusImages, healthStat.GetCurrentStat(), ref lastHealthIndex);
        }

        private void UpdateStatusSprite(Image image, List<Sprite> sprites, float value, ref int lastIndex)
        {
            int currentIndex = value >= 66 ? 0 : value >= 33 ? 1 : 2;
    
            if (currentIndex != lastIndex)
            {
                image.sprite = sprites[currentIndex];
                lastIndex = currentIndex;
            }
        }    
    }
}