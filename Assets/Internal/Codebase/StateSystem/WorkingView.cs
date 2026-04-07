using UnityEngine;
using UnityEngine.UI;

namespace Codebase
{
    public class WorkingView : MonoBehaviour
    {
        [SerializeField] private GameObject progressBarPanel;
        [SerializeField] private Image fillImage;
        
        public void UpdateProgressUI(float progress)
        {
            if (fillImage != null) 
                fillImage.fillAmount = Mathf.Clamp01(progress);
        }

        public void ShowProgressBar(bool show)
        {
            if (progressBarPanel != null)
                progressBarPanel.SetActive(show);
        }
    }
}