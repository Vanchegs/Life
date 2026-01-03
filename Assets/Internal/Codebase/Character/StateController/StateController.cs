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
            UpdateIncreaseStat();
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
            
            Debug.Log(currentState);
        }

        private void UpdateIncreaseStat()
        {
            switch (currentState)
            {
                case States.FoodState:
                    statsController.IncreaseCurrentStat(StatType.FoodStat);
                    break;
                case States.SleepState:
                    statsController.IncreaseCurrentStat(StatType.EnergyStat);
                    break;
                case States.HealthsState:
                    statsController.IncreaseCurrentStat(StatType.HealthStat);
                    break;
                case States.MentalHealthsState:
                    statsController.IncreaseCurrentStat(StatType.MentalHealthStat);
                    break;
                case States.NoneState:
                    break;
            }
        }
    }
}

