using UnityEngine;

namespace Codebase
{
    public class ThoughtController : MonoBehaviour
    {
        [SerializeField] private ThoughtsConfig thoughts;
        
        private enum ThoughtType
        {
            HungryThought,
            EnergyThought,
            MentalThought,
            HealthThought
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

        private void ShowThought()
        {
            
        }
    }
}

