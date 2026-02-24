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
            else if (Input.GetKeyDown(KeyCode.W))
            {
                stateMachine.ChangeState<GamingState>();
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                stateMachine.ChangeState<EatState>();
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                stateMachine.ChangeState<SleepState>();
            }

            Debug.Log(stateMachine.GetCurrentState());
        }


        private void InitStates()
        {
            stateMachine.AddState(new IdleState(this));
            stateMachine.AddState(new SleepState(this));
            stateMachine.AddState(new GamingState(this));
            stateMachine.AddState(new EatState(this));
        }

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

