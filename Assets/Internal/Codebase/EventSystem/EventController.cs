using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Codebase
{
    public class EventController : MonoBehaviour
    {
        [SerializeField] private List<Event> events;

        private void Start()
        {
            Debug.Log(GetEvent().eventName);
        }

        private Event GetEvent()
        {
            var index = Random.Range(0, events.Count);

            return events[index];
        }
    }
}

