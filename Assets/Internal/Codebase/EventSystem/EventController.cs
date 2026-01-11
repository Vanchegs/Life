using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Codebase
{
    public class EventController : MonoBehaviour
    {
        [SerializeField] private List<Event> events;
        
        [SerializeField] private EventView eventView;
        [SerializeField] private StatsController statsController;
        
        private Event currentEvent;

        private void Start() => 
            CreateEvent();

        private Event GetEvent()
        {
            var index = Random.Range(0, events.Count);

            return events[index];
        }

        private void CreateEvent()
        {
            currentEvent = GetEvent();
            eventView.ViewEvent(currentEvent);
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
    }
}

