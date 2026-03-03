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
        private GameEvent currentEvent;
        private int spawnDelay;

        private void Start()
        {
            isEventActive = true;
            spawnDelay = minSpawnDelay;

            StartCoroutine(SpawnEvent());
        }
        
        public void OnSimpleEventButtonClicked()
        {
            if (currentEvent is SimpleEvent simpleEvent)
            {
                statsController.ChangeStatsAfterEvent(
                    simpleEvent.moneyChangeValue,
                    simpleEvent.foodChangeValue,
                    simpleEvent.energyChangeValue,
                    simpleEvent.mentalHealthChangeValue,
                    simpleEvent.healthChangeValue
                );
                
                Debug.Log($"Simple event applied: {simpleEvent.eventName}");
            }
        }
        
        public void OnFirstSolutionButtonClicked()
        {
            if (currentEvent is ChoiceEvent choiceEvent)
            {
                statsController.ChangeStatsAfterEvent(
                    choiceEvent.firstSolutionMoneyChangeValue,
                    choiceEvent.firstSolutionFoodChangeValue,
                    choiceEvent.firstSolutionEnergyValue,
                    choiceEvent.firstSolutionMentalHealthChangeValue,
                    choiceEvent.firstSolutionHealthChangeValue
                );
                
                Debug.Log($"Choice event - first solution: {choiceEvent.eventName}");
            }
        }

        public void OnSecondSolutionButtonClicked()
        {
            if (currentEvent is ChoiceEvent choiceEvent)
            {
                statsController.ChangeStatsAfterEvent(
                    choiceEvent.secondSolutionMoneyChangeValue,
                    choiceEvent.secondSolutionFoodChangeValue,
                    choiceEvent.secondSolutionEnergyValue,
                    choiceEvent.secondSolutionMentalHealthChangeValue,
                    choiceEvent.secondSolutionHealthChangeValue
                );
                
                Debug.Log($"Choice event - second solution: {choiceEvent.eventName}");
            }
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
        
        private GameEvent GetEvent()
        {
            var index = Random.Range(0, eventsList.events.Count);

            return eventsList.events[index];
        }

        private void CreateEvent()
        {
            currentEvent = GetEvent();
            eventView.ShowEvent(currentEvent);
        }
    }
}

