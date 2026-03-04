using DG.Tweening;
using UnityEngine;

namespace Codebase
{
    public class EventView : MonoBehaviour
    {
        [SerializeField] private GameObject choicePanel;
        [SerializeField] private GameObject simplePanel;

        [SerializeField] private ChoiceEventPanel choiceEventPanel;
        [SerializeField] private SimpleEventPanel simpleEventPanel;

        private RectTransform panelTransform;

        public void ShowEvent(GameEvent _event)
        {
            if (_event is SimpleEvent simpleEvent)
            {
                simplePanel.SetActive(true);
                FillSimpleEventPanel(simpleEvent);
                panelTransform = simplePanel.GetComponent<RectTransform>();
            }
            else if (_event is ChoiceEvent choiceEvent)
            {
                choicePanel.SetActive(true);
                FillSolutionEventPanel(choiceEvent);
                panelTransform = choicePanel.GetComponent<RectTransform>();
            }

            if (panelTransform != null)
            {
                panelTransform.anchoredPosition = new Vector2(0, -1000);
                panelTransform.DOAnchorPos(new Vector2(-300, 0), 0.5f)
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

