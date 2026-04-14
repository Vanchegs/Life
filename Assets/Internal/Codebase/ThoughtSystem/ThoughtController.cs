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

        private string GetThought(ThoughtType type)
        {
            string thought = null;
            int randIndex;
            
            switch (type)
            {
                case ThoughtType.HungryThought:
                    randIndex = Random.Range(0, thoughts.hungryThoughts.Count);
                    thought = thoughts.hungryThoughts[randIndex];
                    break;
                case ThoughtType.EnergyThought:
                    randIndex = Random.Range(0, thoughts.energyThoughts.Count);
                    thought = thoughts.energyThoughts[randIndex];
                    break;
                case ThoughtType.HealthThought:
                    randIndex = Random.Range(0, thoughts.healthThoughts.Count);
                    thought = thoughts.healthThoughts[randIndex];
                    break;
                case ThoughtType.MentalThought:
                    randIndex = Random.Range(0, thoughts.mentalThoughts.Count);
                    thought = thoughts.mentalThoughts[randIndex];
                    break;
            }

            return thought;
        }

        private void ShowHungryThought()
        {
            string thought = GetThought(ThoughtType.HungryThought);
            if (!string.IsNullOrEmpty(thought))
                thoughtView.ShowStatThought(thought, thoughtDuration);
        }

        private void ShowEnergyThought()
        {
            string thought = GetThought(ThoughtType.EnergyThought);
            if (!string.IsNullOrEmpty(thought))
                thoughtView.ShowStatThought(thought, thoughtDuration);
        }

        private void ShowMentalThought()
        {
            string thought = GetThought(ThoughtType.MentalThought);
            if (!string.IsNullOrEmpty(thought))
                thoughtView.ShowStatThought(thought, thoughtDuration);
        }

        private void ShowHealthThought()
        {
            string thought = GetThought(ThoughtType.HealthThought);
            if (!string.IsNullOrEmpty(thought))
                thoughtView.ShowStatThought(thought, thoughtDuration);
        }
        
        public void ShowRandomThought()
        {
            ThoughtType[] types = { ThoughtType.HungryThought, ThoughtType.EnergyThought, 
                ThoughtType.MentalThought, ThoughtType.HealthThought };
            
            ThoughtType randomType = types[Random.Range(0, types.Length)];
            
            switch (randomType)
            {
                case ThoughtType.HungryThought:
                    ShowHungryThought();
                    break;
                case ThoughtType.EnergyThought:
                    ShowEnergyThought();
                    break;
                case ThoughtType.MentalThought:
                    ShowMentalThought();
                    break;
                case ThoughtType.HealthThought:
                    ShowHealthThought();
                    break;
            }
        }
    }
}

