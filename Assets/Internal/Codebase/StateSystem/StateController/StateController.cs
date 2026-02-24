using System.Collections;
using UnityEngine;

namespace Codebase
{
    public class StateController : MonoBehaviour
    {
        [SerializeField] private StatsController statsController;

        private StateMachine stateMachine;

        private void Start()
        {
            /*currentState = States.NoneState;*/
            /*StartCoroutine(IncreaseBalance());*/

            stateMachine = new StateMachine();
            InitStates();
            
            stateMachine.ChangeState<IdleState>();
        }

        private void Update()
        {
            CheckInput();
            /*UpdateIncreaseStat();*/
            
            stateMachine.UpdateCurrentState();
        }

        public StatsController GetStatsController() => 
            statsController;

        private void CheckInput()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                stateMachine.ChangeState<WorkState>();
            }
            /*else if (Input.GetKeyDown(KeyCode.W))
                currentState = States.GamingState;
            else if (Input.GetKeyDown(KeyCode.E))
                currentState = States.EatState;
            else if (Input.GetKeyDown(KeyCode.R))
                currentState = States.SleepState;
                */
            
            Debug.Log(stateMachine.GetCurrentState());
        }


        private void InitStates()
        {
            stateMachine.AddState(new IdleState(this));
            stateMachine.AddState(new SleepState(this));
            stateMachine.AddState(new GamingState(this));
            stateMachine.AddState(new EatState(this));
        }

        /*private void UpdateIncreaseStat()
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
        }*/


        public IEnumerator IncreaseBalance()
        {
            while (true)
            { 
                statsController.IncreaseWalletBalance();

                yield return new WaitForSeconds(1);
            }
        }
    }
}

