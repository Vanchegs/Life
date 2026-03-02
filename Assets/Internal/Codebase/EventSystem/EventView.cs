using DG.Tweening;
using UnityEngine;

namespace Codebase
{
    public class EventView : MonoBehaviour
    {
        [SerializeField] private GameObject eventPanel;
        [SerializeField] private RectTransform panelTransform;

        [SerializeField] private SolutionEventPanel solutionEventPanel;
        [SerializeField] private SimpleEventPanel simpleEventPanel;
        
        public void ShowEvent(GameEvent _event)
        {
            /*eventNameText.text = _event.eventName;
            eventDescriptionText.text = _event.description;*/
            /*firstSolutionText.text = _event.firstSolution;
            secondSolutionText.text = _event.secondSolution;*/
    
            eventPanel.SetActive(true);
    
            panelTransform.anchoredPosition = new Vector2(0, -1000);

            var finishPosition = new Vector2(-300, 0);
            
            panelTransform.DOAnchorPos(finishPosition, 0.5f)
                .SetEase(Ease.OutBack);
        }

        private void FillSolutionEventPanel(ChoiceEvent choiceEvent) => 
            solutionEventPanel.SetText(choiceEvent.eventName, choiceEvent.description, 
                choiceEvent.firstSolution, choiceEvent.secondSolution);

        private void FillSimpleEventPanel(SimpleEvent simpleEvent) => 
            simpleEventPanel.SetText(simpleEvent.eventName, simpleEvent.description, simpleEvent.buttonText);

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

