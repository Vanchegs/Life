using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Codebase
{
    public class EventView : MonoBehaviour
    {
        [SerializeField] private GameObject eventPanel;
        [SerializeField] private RectTransform panelTransform;
        
        [SerializeField] private TMP_Text eventNameText;
        [SerializeField] private TMP_Text eventDescriptionText;
        [SerializeField] private TMP_Text firstSolutionText;
        [SerializeField] private TMP_Text secondSolutionText;
        
        public void ShowEvent(Event _event)
        {
            eventNameText.text = _event.eventName;
            eventDescriptionText.text = _event.description;
            firstSolutionText.text = _event.firstSolution;
            secondSolutionText.text = _event.secondSolution;
    
            eventPanel.SetActive(true);
    
            panelTransform.anchoredPosition = new Vector2(0, -1000);

            var finishPosition = new Vector2(-300, 0);
            
            panelTransform.DOAnchorPos(finishPosition, 0.5f)
                .SetEase(Ease.OutBack);
        }

        public void HideEvent()
        {
            var finishPosition = new Vector2(-300, 1200);

            panelTransform.DOAnchorPos(finishPosition, 0.5f)
                .SetEase(Ease.InBack)
                .OnComplete(() => {
                    eventPanel.SetActive(false);
                });
        }
    }
}

