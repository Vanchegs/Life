using UnityEngine;
using Random = UnityEngine.Random;

namespace Codebase
{
    public class ThoughtController : MonoBehaviour
    {
        [SerializeField] private ThoughtsConfig thoughts;
        [SerializeField] private ThoughtView thoughtView;
        [SerializeField] private float thoughtDuration = 5f;
        
        // Флаги, чтобы мысль показывалась только 1 раз за падение
        private bool hasShownHungryThought;
        private bool hasShownEnergyThought;
        private bool hasShownMentalThought;
        private bool hasShownHealthThought;
        
        private enum ThoughtType
        {
            HungryThought,
            EnergyThought,
            MentalThought,
            HealthThought
        }

        private void Start()
        {
            GameEventBus.OnFellMental += ShowMentalThought;
            GameEventBus.OnFellEnergy += ShowEnergyThought;
            GameEventBus.OnFellHealth += ShowHealthThought;
            GameEventBus.OnFellHungry += ShowHungryThought;
        }

        private void OnDisable()
        {
            GameEventBus.OnFellMental -= ShowMentalThought;
            GameEventBus.OnFellEnergy -= ShowEnergyThought;
            GameEventBus.OnFellHealth -= ShowHealthThought;
            GameEventBus.OnFellHungry -= ShowHungryThought;
        }
        
        // Метод для сброса флагов (когда стат восстановился)
        public void ResetHungryFlag() => hasShownHungryThought = false;
        public void ResetEnergyFlag() => hasShownEnergyThought = false;
        public void ResetMentalFlag() => hasShownMentalThought = false;
        public void ResetHealthFlag() => hasShownHealthThought = false;
        
        // Сбросить все флаги сразу
        public void ResetAllFlags()
        {
            hasShownHungryThought = false;
            hasShownEnergyThought = false;
            hasShownMentalThought = false;
            hasShownHealthThought = false;
        }

        private string GetRandomThought(ThoughtType type)
        {
            switch (type)
            {
                case ThoughtType.HungryThought:
                    if (thoughts.hungryThoughts == null || thoughts.hungryThoughts.Count == 0) return null;
                    return thoughts.hungryThoughts[Random.Range(0, thoughts.hungryThoughts.Count)];
                    
                case ThoughtType.EnergyThought:
                    if (thoughts.energyThoughts == null || thoughts.energyThoughts.Count == 0) return null;
                    return thoughts.energyThoughts[Random.Range(0, thoughts.energyThoughts.Count)];
                    
                case ThoughtType.MentalThought:
                    if (thoughts.mentalThoughts == null || thoughts.mentalThoughts.Count == 0) return null;
                    return thoughts.mentalThoughts[Random.Range(0, thoughts.mentalThoughts.Count)];
                    
                case ThoughtType.HealthThought:
                    if (thoughts.healthThoughts == null || thoughts.healthThoughts.Count == 0) return null;
                    return thoughts.healthThoughts[Random.Range(0, thoughts.healthThoughts.Count)];
                    
                default:
                    return null;
            }
        }

        private void ShowHungryThought()
        {
            if (hasShownHungryThought) return;
            hasShownHungryThought = true;
            
            string thought = GetRandomThought(ThoughtType.HungryThought);
            if (!string.IsNullOrEmpty(thought))
                thoughtView.ShowStatThought(thought, thoughtDuration);
        }

        private void ShowEnergyThought()
        {
            if (hasShownEnergyThought) return;
            hasShownEnergyThought = true;
            
            string thought = GetRandomThought(ThoughtType.EnergyThought);
            if (!string.IsNullOrEmpty(thought))
                thoughtView.ShowStatThought(thought, thoughtDuration);
        }

        private void ShowMentalThought()
        {
            if (hasShownMentalThought) return;
            hasShownMentalThought = true;
            
            string thought = GetRandomThought(ThoughtType.MentalThought);
            if (!string.IsNullOrEmpty(thought))
                thoughtView.ShowStatThought(thought, thoughtDuration);
        }

        private void ShowHealthThought()
        {
            if (hasShownHealthThought) return;
            hasShownHealthThought = true;
            
            string thought = GetRandomThought(ThoughtType.HealthThought);
            if (!string.IsNullOrEmpty(thought))
                thoughtView.ShowStatThought(thought, thoughtDuration);
        }
    }
}