using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase
{
    public class WorkState : State
    {
        private int timeForOrder = 8;
        
        private GameObject progressBarPanel;
        private Image fillImage;
        
        private float workProgress;
        private Coroutine workCoroutine;

        public WorkState(StateSwitcher stateController, GameObject progressBarPanel, Image fillImage) : base(stateController)
        {
            this.progressBarPanel = progressBarPanel;
            this.fillImage = fillImage;
        }

        public override void Enter()
        {
            ShowProgressBar(true);
            UpdateProgressUI(workProgress);
            StartWorkCoroutine();
            Debug.Log(workProgress);
        }

        public override void Update()
        {
        }

        public override void Exit()
        {
            ShowProgressBar(false);
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
                UpdateProgressUI(progress);
                
                if (workProgress >= timeForOrder)
                {
                    StateController.WalletController.IncreaseWalletBalance();
                    workProgress = 0f;
                }
            }
        }

        private void UpdateProgressUI(float progress)
        {
            if (fillImage != null)
            {
                fillImage.fillAmount = Mathf.Clamp01(progress);
            }
        }

        private void ShowProgressBar(bool show)
        {
            if (progressBarPanel != null)
                progressBarPanel.SetActive(show);
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