using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Codebase
{
    public class EventController : MonoBehaviour
    {
        [SerializeField] private EventsList eventsList;
        
        [SerializeField] private EventView eventView;
        [SerializeField] private StatsController statsController;
        
        [SerializeField] private int minSpawnDelay, maxSpawnDelay;

        private bool isEventActive;
        private Event currentEvent;
        private int spawnDelay;

        private void Start()
        {
            isEventActive = true;
            spawnDelay = minSpawnDelay;

            StartCoroutine(SpawnEvent());
        }

        public void ClickFirstSolutionButton()
        {
            statsController.ChangeStatsAfterEvent(currentEvent.firstSolutionFoodChangeValue, currentEvent.firstSolutionEnergyValue, 
                currentEvent.firstSolutionMentalHealthChangeValue, currentEvent.firstSolutionHealthChangeValue);
        }
        
        public void ClickSecondSolutionButton()
        {
            statsController.ChangeStatsAfterEvent(currentEvent.secondSolutionFoodChangeValue, currentEvent.secondSolutionEnergyValue,
                currentEvent.secondSolutionMentalHealthChangeValue, currentEvent.secondSolutionHealthChangeValue);
        }

        private IEnumerator SpawnEvent()
        {
            while (isEventActive)
            {
                yield return new WaitForSeconds(spawnDelay);

                CreateEvent();

                spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
            }
        }
        
        private Event GetEvent()
        {
            var index = Random.Range(0, eventsList.events.Count);

            return eventsList.events[index];
        }

        private void CreateEvent()
        {
            currentEvent = GetEvent();
            eventView.ViewEvent(currentEvent);
        }
    }
}

