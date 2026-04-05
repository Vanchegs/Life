using System.Collections;
using UnityEngine;

namespace Codebase
{
    public class WorkState : State
    {
        public WorkState(StateSwitcher stateController) : base(stateController) { }

        private int timeForOrder;
        private float currentTime;
        
        private Coroutine workCoroutine;

        public override void Enter()
        {
            StartWorkCoroutine();
        }

        public override void Update()
        {
            
        }

        public override void Exit()
        {
            StopWorkCoroutine();
        }
        
        private IEnumerator MoneyAccrual()
        {
            while (true)
            { 
                yield return new WaitForSeconds(1);
                
                StateController.WalletController.IncreaseWalletBalance();
                
                Debug.Log("slergnir");
            }
        }

        private void StartWorkCoroutine()
        {
            if (workCoroutine != null)
                return;

            workCoroutine = StateController.StartCoroutine(MoneyAccrual());
        }

        private void StopWorkCoroutine()
        {
            if (workCoroutine != null)
            {
                StateController.StopCoroutine(workCoroutine);
                workCoroutine = null;
            }
        }
    }
}