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
        
        private Coroutine autoHideCoroutine;

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
                if (autoHideCoroutine != null)
                {
                    StopCoroutine(autoHideCoroutine);
                    autoHideCoroutine = null;
                }
                
                statsController.ChangeStats(
                    simpleEvent.moneyChangeValue,
                    simpleEvent.foodChangeValue,
                    simpleEvent.energyChangeValue,
                    simpleEvent.mentalHealthChangeValue,
                    simpleEvent.healthChangeValue
                );
                
                isEventExist = false;
                eventView.HideEvent(currentEvent);
                currentEvent = null;
                
                Debug.Log($"Simple event applied: {simpleEvent.eventName}");
            }
        }
        
        public void OnFirstSolutionButtonClicked()
        {
            if (currentEvent is ChoiceEvent choiceEvent)
            {
                if (autoHideCoroutine != null)
                {
                    StopCoroutine(autoHideCoroutine);
                    autoHideCoroutine = null;
                }
                
                statsController.ChangeStats(
                    choiceEvent.firstSolutionMoneyChangeValue,
                    choiceEvent.firstSolutionFoodChangeValue,
                    choiceEvent.firstSolutionEnergyValue,
                    choiceEvent.firstSolutionMentalHealthChangeValue,
                    choiceEvent.firstSolutionHealthChangeValue
                );
                
                isEventExist = false;
                eventView.HideEvent(currentEvent);
                currentEvent = null;
                
                Debug.Log($"Choice event - first solution: {choiceEvent.eventName}");
            }
        }

        public void OnSecondSolutionButtonClicked()
        {
            if (currentEvent is ChoiceEvent choiceEvent)
            {
                if (autoHideCoroutine != null)
                {
                    StopCoroutine(autoHideCoroutine);
                    autoHideCoroutine = null;
                }
                
                statsController.ChangeStats(
                    choiceEvent.secondSolutionMoneyChangeValue,
                    choiceEvent.secondSolutionFoodChangeValue,
                    choiceEvent.secondSolutionEnergyValue,
                    choiceEvent.secondSolutionMentalHealthChangeValue,
                    choiceEvent.secondSolutionHealthChangeValue
                );

                isEventExist = false;
                eventView.HideEvent(currentEvent);
                currentEvent = null;
                
                Debug.Log($"Choice event - second solution: {choiceEvent.eventName}");
            }
        }

        private void HideCurrentEvent()
        {
            if (currentEvent != null)
            {
                if (autoHideCoroutine != null)
                {
                    StopCoroutine(autoHideCoroutine);
                    autoHideCoroutine = null;
                }
                
                isEventExist = false;
                eventView.HideEvent(currentEvent);
                currentEvent = null;
            }
        }

        private IEnumerator AutoHideCoroutine()
        {
            yield return new WaitForSeconds(10f);
            HideCurrentEvent();
            autoHideCoroutine = null;
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
            if (autoHideCoroutine != null)
            {
                StopCoroutine(autoHideCoroutine);
                autoHideCoroutine = null;
            }
            
            currentEvent = eventSelector.GetEvent();
            eventView.ShowEvent(currentEvent);
            isEventExist = true;
            autoHideCoroutine = StartCoroutine(AutoHideCoroutine());
        }
    }
}