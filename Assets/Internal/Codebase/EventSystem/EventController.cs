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
        [SerializeField] private float autoSelectDelay = 10f;

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
                StopAutoHideCoroutine();
                
                statsController.ChangeStats(
                    simpleEvent.moneyChangeValue,
                    simpleEvent.foodChangeValue,
                    simpleEvent.energyChangeValue,
                    simpleEvent.mentalHealthChangeValue,
                    simpleEvent.healthChangeValue
                );
                
                CloseEvent();
                
                Debug.Log($"Simple event applied: {simpleEvent.eventName}");
            }
        }
        
        public void OnFirstSolutionButtonClicked()
        {
            if (currentEvent is ChoiceEvent choiceEvent)
            {
                StopAutoHideCoroutine();
                
                statsController.ChangeStats(
                    choiceEvent.firstSolutionMoneyChangeValue,
                    choiceEvent.firstSolutionFoodChangeValue,
                    choiceEvent.firstSolutionEnergyValue,
                    choiceEvent.firstSolutionMentalHealthChangeValue,
                    choiceEvent.firstSolutionHealthChangeValue
                );
                
                CloseEvent();
                
                Debug.Log($"Choice event - first solution: {choiceEvent.eventName}");
            }
        }

        public void OnSecondSolutionButtonClicked()
        {
            if (currentEvent is ChoiceEvent choiceEvent)
            {
                StopAutoHideCoroutine();
                
                statsController.ChangeStats(
                    choiceEvent.secondSolutionMoneyChangeValue,
                    choiceEvent.secondSolutionFoodChangeValue,
                    choiceEvent.secondSolutionEnergyValue,
                    choiceEvent.secondSolutionMentalHealthChangeValue,
                    choiceEvent.secondSolutionHealthChangeValue
                );

                CloseEvent();
                
                Debug.Log($"Choice event - second solution: {choiceEvent.eventName}");
            }
        }

        private void StopAutoHideCoroutine()
        {
            if (autoHideCoroutine != null)
            {
                StopCoroutine(autoHideCoroutine);
                autoHideCoroutine = null;
            }
        }

        private void CloseEvent()
        {
            isEventExist = false;
            eventView.HideEvent(currentEvent);
            currentEvent = null;
        }

        private IEnumerator AutoHideCoroutine()
        {
            yield return new WaitForSeconds(autoSelectDelay);
            
            if (currentEvent != null)
            {
                Debug.Log("Время вышло! Автоматический выбор...");
                
                if (currentEvent is SimpleEvent simpleEvent)
                {
                    statsController.ChangeStats(
                        simpleEvent.moneyChangeValue,
                        simpleEvent.foodChangeValue,
                        simpleEvent.energyChangeValue,
                        simpleEvent.mentalHealthChangeValue,
                        simpleEvent.healthChangeValue
                    );
                    
                    Debug.Log($"Simple event auto-applied: {simpleEvent.eventName}");
                }
                else if (currentEvent is ChoiceEvent choiceEvent)
                {
                    int randomChoice = Random.Range(0, 2);
                    
                    if (randomChoice == 0)
                    {
                        statsController.ChangeStats(
                            choiceEvent.firstSolutionMoneyChangeValue,
                            choiceEvent.firstSolutionFoodChangeValue,
                            choiceEvent.firstSolutionEnergyValue,
                            choiceEvent.firstSolutionMentalHealthChangeValue,
                            choiceEvent.firstSolutionHealthChangeValue
                        );
                        Debug.Log($"Choice event auto-selected: first solution for {choiceEvent.eventName}");
                    }
                    else
                    {
                        statsController.ChangeStats(
                            choiceEvent.secondSolutionMoneyChangeValue,
                            choiceEvent.secondSolutionFoodChangeValue,
                            choiceEvent.secondSolutionEnergyValue,
                            choiceEvent.secondSolutionMentalHealthChangeValue,
                            choiceEvent.secondSolutionHealthChangeValue
                        );
                        Debug.Log($"Choice event auto-selected: second solution for {choiceEvent.eventName}");
                    }
                }
                
                CloseEvent();
            }
            
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
            StopAutoHideCoroutine();
            
            currentEvent = eventSelector.GetEvent();
            eventView.ShowEvent(currentEvent);
            isEventExist = true;
            autoHideCoroutine = StartCoroutine(AutoHideCoroutine());
        }
    }
}