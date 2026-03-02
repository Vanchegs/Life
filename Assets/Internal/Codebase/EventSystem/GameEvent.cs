using UnityEngine;

namespace Codebase
{
    public abstract class GameEvent : ScriptableObject
    {
        [Header("Info")] 
        public string eventName;
        public string description;
    }
}