using System.Collections.Generic;
using UnityEngine;

namespace Codebase
{
    [CreateAssetMenu(menuName = "Configs/Events List", fileName = "EventsList")]
    public class EventsList : ScriptableObject
    {
        public List<Event> events;
    }
}

