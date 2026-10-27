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
                ApplySimpleEvent(simpleEvent);
            }
        }

        public void OnFirstSolutionButtonClicked()
        {
            if (currentEvent is ChoiceEvent choiceEvent)
            {
                StopAutoHideCoroutine();
                ApplyChoiceEvent(choiceEvent, true);
            }
        }

        public void OnSecondSolutionButtonClicked()
        {
            if (currentEvent is ChoiceEvent choiceEvent)
            {
                StopAutoHideCoroutine();
                ApplyChoiceEvent(choiceEvent, false);
            }
        }

        private void ApplySimpleEvent(SimpleEvent simpleEvent)
        {
            statsController.ChangeStats(
                moneyChangeValue: simpleEvent.moneyChangeValue,
                foodChangeValue: simpleEvent.foodChangeValue,
                energyChangeValue: simpleEvent.energyChangeValue,
                mentalHealthChangeValue: simpleEvent.mentalHealthChangeValue,
                healthChangeValue: simpleEvent.healthChangeValue);

            CloseEvent();
        }

        private void ApplyChoiceEvent(ChoiceEvent choiceEvent, bool isFirstSolution)
        {
            statsController.ChangeStats(
                moneyChangeValue: isFirstSolution ? choiceEvent.firstSolutionMoneyChangeValue : choiceEvent.secondSolutionMoneyChangeValue,
                foodChangeValue: isFirstSolution ? choiceEvent.firstSolutionFoodChangeValue : choiceEvent.secondSolutionFoodChangeValue,
                energyChangeValue: isFirstSolution ? choiceEvent.firstSolutionEnergyValue : choiceEvent.secondSolutionEnergyValue,
                mentalHealthChangeValue: isFirstSolution ? choiceEvent.firstSolutionMentalHealthChangeValue : choiceEvent.secondSolutionMentalHealthChangeValue,
                healthChangeValue: isFirstSolution ? choiceEvent.firstSolutionHealthChangeValue : choiceEvent.secondSolutionHealthChangeValue);

            CloseEvent();
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

            autoHideCoroutine = null;

            if (currentEvent is SimpleEvent simpleEvent)
                ApplySimpleEvent(simpleEvent);
            else if (currentEvent is ChoiceEvent choiceEvent)
                ApplyChoiceEvent(choiceEvent, Random.Range(0, 2) == 0);
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
