using UnityEngine;

namespace Codebase
{
    public class StateSwitcher : MonoBehaviour
    {
        [SerializeField] private StatsController statsController;
        [SerializeField] private WalletController walletController;

        [SerializeField] private WorkingView workingView;
        
        private StateMachine stateMachine;

        public WalletController WalletController => walletController;

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
            stateMachine.AddState(new WorkState(this, workingView));
        }
    }
}

