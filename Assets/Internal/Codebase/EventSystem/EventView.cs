using DG.Tweening;
using UnityEngine;

namespace Codebase
{
    public class EventView : MonoBehaviour
    {
        [SerializeField] private GameObject choicePanel;
        [SerializeField] private GameObject simplePanel;
        [SerializeField] private RectTransform panelTransform;

        [SerializeField] private ChoiceEventPanel choiceEventPanel;
        [SerializeField] private SimpleEventPanel simpleEventPanel;
        
        public void ShowEvent(GameEvent _event)
        {
            RectTransform targetPanel = null;
    
            if (_event is SimpleEvent simpleEvent)
            {
                simplePanel.SetActive(true);
                FillSimpleEventPanel(simpleEvent);
                targetPanel = simplePanel.GetComponent<RectTransform>();
            }
            else if (_event is ChoiceEvent choiceEvent)
            {
                choicePanel.SetActive(true);
                FillSolutionEventPanel(choiceEvent);
                targetPanel = choicePanel.GetComponent<RectTransform>();
            }

            if (targetPanel != null)
            {
                targetPanel.anchoredPosition = new Vector2(0, -1000);
                targetPanel.DOAnchorPos(new Vector2(-300, 0), 0.5f)
                    .SetEase(Ease.OutBack);
            }
        }

        private void FillSolutionEventPanel(ChoiceEvent choiceEvent) => 
            choiceEventPanel.SetText(choiceEvent.eventName, choiceEvent.description, 
                choiceEvent.firstSolution, choiceEvent.secondSolution);

        private void FillSimpleEventPanel(SimpleEvent simpleEvent) => 
            simpleEventPanel.SetText(simpleEvent.eventName, simpleEvent.description, simpleEvent.buttonText);

        public void HideEvent(GameObject eventPanel)
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

