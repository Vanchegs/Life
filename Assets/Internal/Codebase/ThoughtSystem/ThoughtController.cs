using System.Collections;
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
            HealthThought,
            RandomThought
        }

        private void Start()
        {
            GameEventBus.OnFellMental += ShowMentalThought;
            GameEventBus.OnFellEnergy += ShowEnergyThought;
            GameEventBus.OnFellHealth += ShowHealthThought;
            GameEventBus.OnFellHungry += ShowHungryThought;

            StartCoroutine(RegularRandomThoughtShowing());
        }

        private void OnDisable()
        {
            GameEventBus.OnFellMental -= ShowMentalThought;
            GameEventBus.OnFellEnergy -= ShowEnergyThought;
            GameEventBus.OnFellHealth -= ShowHealthThought;
            GameEventBus.OnFellHungry -= ShowHungryThought;
        }

        private IEnumerator RegularRandomThoughtShowing()
        {
            while (true)
            {
                var randSeconds = Random.Range(0, 20);
                
                yield return new WaitForSecondsRealtime(randSeconds);
                
                thoughtView.ShowRandomThought(GetRandomThought(ThoughtType.RandomThought));
            }
        }

        private string GetRandomThought(ThoughtType type)
        {
            int randIndex;
            
            switch (type)
            {
                case ThoughtType.HungryThought:
                    randIndex = Random.Range(0, thoughts.hungryThoughts.Count);
                    return thoughts.hungryThoughts[randIndex];
                case ThoughtType.EnergyThought:
                    randIndex = Random.Range(0, thoughts.energyThoughts.Count);
                    return thoughts.energyThoughts[randIndex];
                case ThoughtType.MentalThought:
                    randIndex = Random.Range(0, thoughts.mentalThoughts.Count);
                    return thoughts.mentalThoughts[randIndex];
                case ThoughtType.HealthThought:
                    randIndex = Random.Range(0, thoughts.healthThoughts.Count);
                    return thoughts.healthThoughts[randIndex];
                case ThoughtType.RandomThought:
                    randIndex = Random.Range(0, thoughts.healthThoughts.Count);
                    return thoughts.randomThoughts[randIndex];
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
    }
}