using System.Collections;
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
            StartCoroutine(IncreaseBalance());
        }

        private void Update()
        {
            CheckInput();
            UpdateIncreaseStat();
        }

        private void CheckInput()
        {
            if (Input.GetKeyDown(KeyCode.Q))
                currentState = States.WorkState;
            else if (Input.GetKeyDown(KeyCode.W))
                currentState = States.GamingState;
            else if (Input.GetKeyDown(KeyCode.E))
                currentState = States.EatState;
            else if (Input.GetKeyDown(KeyCode.R))
                currentState = States.SleepState;
            
            Debug.Log(currentState);
        }

        private void UpdateIncreaseStat()
        {
            switch (currentState)
            {
                case States.EatState:
                    statsController.IncreaseCurrentStat(StatType.FoodStat);
                    break;
                case States.SleepState:
                    statsController.IncreaseCurrentStat(StatType.HealthStat);
                    statsController.IncreaseCurrentStat(StatType.EnergyStat);
                    break;
                case States.GamingState:
                    statsController.IncreaseCurrentStat(StatType.MentalHealthStat);
                    break;
                case States.WorkState:
                    break;
                case States.NoneState:
                    break;
            }
        }

        private IEnumerator IncreaseBalance()
        {
            while (true)
            {
                if (currentState == States.WorkState) 
                    statsController.IncreaseWalletBalance();
                
                yield return new WaitForSeconds(1);
            }
        }
    }
}

