using System.Collections.Generic;
using UnityEngine;

namespace Codebase
{
    public class EventController : MonoBehaviour
    {
        [SerializeField] private List<Event> events;

        private Event GetEvent()
        {
            var index = Random.Range(0, events.Count);

            return events[index];
        }
    }
}

