using System.Collections;
using UnityEngine;

namespace Codebase
{
    public class WorkState : State
    {
        private int timeForOrder = 8;
        
        private float workProgress;
        private Coroutine workCoroutine;
        private WorkingView workingView;

        public WorkState(StateSwitcher stateController, WorkingView workingView) : base(stateController)
        {
            this.workingView = workingView;
        }

        public override void Enter()
        {
            workingView.ShowProgressBar(true);
            workingView.UpdateProgressUI(workProgress);
            StartWorkCoroutine();
            Debug.Log(workProgress);
        }

        public override void Update()
        {
        }

        public override void Exit()
        {
            workingView.ShowProgressBar(false);
            StopWorkCoroutine();
            Debug.Log(workProgress);
        }
        
        private IEnumerator MoneyAccrual()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.1f);
                
                workProgress += 0.1f;
                
                float progress = workProgress / timeForOrder;
                workingView.UpdateProgressUI(progress);
                
                if (workProgress >= timeForOrder)
                {
                    StateController.WalletController.IncreaseWalletBalance();
                    workProgress = 0f;
                }
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