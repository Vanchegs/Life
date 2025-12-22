using System;
using UnityEngine;

namespace Codebase
{
    public class StateController : MonoBehaviour
    {
        [SerializeField] private StatsController statsController;

        private States currentState;

        private void Start()
        {
            currentState = States.NoneState;
        }

        private void Update()
        {
            CheckInput();
        }

        private void CheckInput()
        {
            if (Input.GetKeyDown(KeyCode.Q))
                currentState = States.SleepState;
            else if (Input.GetKeyDown(KeyCode.W))
                currentState = States.FoodState;
            else if (Input.GetKeyDown(KeyCode.E))
                currentState = States.HealthsState;
            else if (Input.GetKeyDown(KeyCode.R))
                currentState = States.MentalHealthsState;
            else
                currentState = States.NoneState;
        }

        private void UpdateIncreaseStat()
        {
            statsController.IncreaseCurrentStat(StatType.FoodStat);
            statsController.IncreaseCurrentStat(StatType.HealthStat);
            statsController.IncreaseCurrentStat(StatType.MentalHealthStat);
            statsController.IncreaseCurrentStat(StatType.SleepStat);
        }
    }
}

