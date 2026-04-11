using System.Collections.Generic;
using UnityEngine;

namespace Codebase
{
    [CreateAssetMenu(menuName = "Configs/Thoughts Config", fileName = "ThoughtsConfig")]
    public class ThoughtsConfig : ScriptableObject
    {
        public List<string> hungryThoughts;
        public List<string> mentalThoughts;
        public List<string> healthThoughts;
        public List<string> energyThoughts;
    }
}
