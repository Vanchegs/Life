using UnityEngine;

namespace Codebase
{
    public class EventSelector
    {
        private EventsList choiceEvents;
        private EventsList simpleEvents;
        
        public EventSelector(EventsList choiceEvents, EventsList simpleEvents)
        {
            this.choiceEvents = choiceEvents;
            this.simpleEvents = simpleEvents;
        }

        public GameEvent GetEvent()
        {
            var randomList = Random.Range(0, 1);

            switch (randomList)
            {
                case 0:
                    var randomChoiceIndex = Random.Range(0, choiceEvents.events.Count);
                    return choiceEvents.events[randomChoiceIndex];
                case 1:
                    var randomSimpleIndex = Random.Range(0, simpleEvents.events.Count);
                    return simpleEvents.events[randomSimpleIndex];
                default:
                    return null;
            }
        }
    }
}

