using System.Collections;
using UnityEngine;

namespace Codebase
{
    public class StateController : MonoBehaviour
    {
        [SerializeField] private StatsController statsController;
        [SerializeField] private WalletController walletController;
        
        private StateMachine stateMachine;
        
        private Coroutine workCoroutine;

        private void Start()
        {
            stateMachine = new StateMachine();
            InitStates();
            
            stateMachine.ChangeState<IdleState>();
        }

        private void Update()
        {
            CheckInput();
            
            stateMachine.UpdateCurrentState();
        }

        public StatsController GetStatsController() => 
            statsController;
        
        public void StartWorkCoroutine()
        {
            if (workCoroutine != null)
                return;

            workCoroutine = StartCoroutine(MoneyAccrual());
        }

        public void StopWorkCoroutine()
        {
            if (workCoroutine != null)
            {
                StopCoroutine(workCoroutine);
                workCoroutine = null;
            }
        }

        private void CheckInput()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (stateMachine.GetCurrentState() == typeof(WorkState))
                    return;
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
        }


        private void InitStates()
        {
            stateMachine.AddState(new IdleState(this));
            stateMachine.AddState(new SleepState(this));
            stateMachine.AddState(new GamingState(this));
            stateMachine.AddState(new EatState(this));
            stateMachine.AddState(new WorkState(this));
        }

        private IEnumerator MoneyAccrual()
        {
            while (true)
            { 
                yield return new WaitForSeconds(1);
                
                walletController.IncreaseWalletBalance();
            }
        }
    }
}

