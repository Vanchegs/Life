using System.Collections;
using UnityEngine;

namespace Codebase
{
    public class StateController : MonoBehaviour
    {
        [SerializeField] private StatsController statsController;

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
            /*UpdateIncreaseStat();*/
            
            stateMachine.UpdateCurrentState();
        }

        public StatsController GetStatsController() => 
            statsController;
        
        public void StartWorkCoroutine()
        {
            if (workCoroutine != null)
            {
                Debug.Log("Работа уже идет!");
                return;
            }
        
            workCoroutine = StartCoroutine(MoneyAccrual());
            Debug.Log("Корутина работы ЗАПУЩЕНА");
        }

        public void StopWorkCoroutine()
        {
            StopCoroutine(workCoroutine);
        }

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
            stateMachine.AddState(new WorkState(this));
        }

        private IEnumerator MoneyAccrual()
        {
            while (true)
            { 
                statsController.IncreaseWalletBalance();

                yield return new WaitForSeconds(1);
            }
        }
    }
}

