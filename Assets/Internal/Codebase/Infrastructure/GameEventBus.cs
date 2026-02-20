using System;
using UnityEngine;

namespace Codebase
{
    public class GameEventBus : MonoBehaviour
    {
        public static Action<int> OnUpdateBalance;
    }
}

