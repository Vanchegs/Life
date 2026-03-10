using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Codebase
{
    public class EventController : MonoBehaviour
    {
        [SerializeField] private EventsList choiceEvents;
        [SerializeField] private EventsList simpleEvents;
        
        [SerializeField] private EventView eventView;
        [SerializeField] private StatsController statsController;
        
        [SerializeField] private int minSpawnDelay, maxSpawnDelay;

        private bool isEventActive;
        private bool isEventExist;
        private GameEvent currentEvent;
        private int spawnDelay;
        private EventSelector eventSelector;

        private void Start()
        {
            isEventActive = true;
            spawnDelay = minSpawnDelay;

            eventSelector = new EventSelector(choiceEvents, simpleEvents);
            
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
                
                isEventExist = false;
                
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
                
                isEventExist = false;
                
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

                isEventExist = false;
                
                Debug.Log($"Choice event - second solution: {choiceEvent.eventName}");
            }
        }

        private IEnumerator SpawnEvent()
        {
            while (isEventActive)
            {
                if (isEventExist)
                {
                    yield return new WaitForSeconds(1f);
                    continue;
                }
        
                yield return new WaitForSeconds(spawnDelay);

                CreateEvent();
                spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
            }
        }

        private void CreateEvent()
        {
            currentEvent = eventSelector.GetEvent();
            eventView.ShowEvent(currentEvent);
            isEventExist = true;
        }
    }
}

