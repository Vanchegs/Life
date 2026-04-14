using UnityEngine;
using Random = UnityEngine.Random;

namespace Codebase
{
    public class ThoughtController : MonoBehaviour
    {
        [SerializeField] private ThoughtsConfig thoughts;
        [SerializeField] private ThoughtView thoughtView;
        [SerializeField] private float thoughtDuration = 3f;
        
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

        private string GetRandomThought(ThoughtType type)
        {
            int randIndex;
            
            switch (type)
            {
                case ThoughtType.HungryThought:
                    if (thoughts.hungryThoughts == null || thoughts.hungryThoughts.Count == 0) return null;
                    randIndex = Random.Range(0, thoughts.hungryThoughts.Count);
                    return thoughts.hungryThoughts[randIndex];
                    
                case ThoughtType.EnergyThought:
                    if (thoughts.energyThoughts == null || thoughts.energyThoughts.Count == 0) return null;
                    randIndex = Random.Range(0, thoughts.energyThoughts.Count);
                    return thoughts.energyThoughts[randIndex];
                    
                case ThoughtType.MentalThought:
                    if (thoughts.mentalThoughts == null || thoughts.mentalThoughts.Count == 0) return null;
                    randIndex = Random.Range(0, thoughts.mentalThoughts.Count);
                    return thoughts.mentalThoughts[randIndex];
                    
                case ThoughtType.HealthThought:
                    if (thoughts.healthThoughts == null || thoughts.healthThoughts.Count == 0) return null;
                    randIndex = Random.Range(0, thoughts.healthThoughts.Count);
                    return thoughts.healthThoughts[randIndex];
                    
                default:
                    return null;
            }
        }

        private void ShowHungryThought()
        {
            string thought = GetRandomThought(ThoughtType.HungryThought);
            if (!string.IsNullOrEmpty(thought))
                thoughtView.ShowStatThought(thought, thoughtDuration);
        }

        private void ShowEnergyThought()
        {
            string thought = GetRandomThought(ThoughtType.EnergyThought);
            if (!string.IsNullOrEmpty(thought))
                thoughtView.ShowStatThought(thought, thoughtDuration);
        }

        private void ShowMentalThought()
        {
            string thought = GetRandomThought(ThoughtType.MentalThought);
            if (!string.IsNullOrEmpty(thought))
                thoughtView.ShowStatThought(thought, thoughtDuration);
        }

        private void ShowHealthThought()
        {
            string thought = GetRandomThought(ThoughtType.HealthThought);
            if (!string.IsNullOrEmpty(thought))
                thoughtView.ShowStatThought(thought, thoughtDuration);
        }
        
        public void ShowRandomStatThought()
        {
            ThoughtType[] types = { ThoughtType.HungryThought, ThoughtType.EnergyThought, 
                ThoughtType.MentalThought, ThoughtType.HealthThought };
            
            ThoughtType randomType = types[Random.Range(0, types.Length)];
            
            string thought = GetRandomThought(randomType);
            if (!string.IsNullOrEmpty(thought))
                thoughtView.ShowStatThought(thought, thoughtDuration);
        }
    }
}