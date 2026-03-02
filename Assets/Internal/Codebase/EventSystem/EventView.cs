using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

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
            /*eventNameText.text = _event.eventName;
            eventDescriptionText.text = _event.description;*/
            /*firstSolutionText.text = _event.firstSolution;
            secondSolutionText.text = _event.secondSolution;*/

            if (_event.GetType() == typeof(SimpleEvent))
            {
                simplePanel.SetActive(true);
                
                FillSimpleEventPanel((SimpleEvent)_event);
            }
            else if (_event.GetType() == typeof(ChoiceEvent))
            {
                choicePanel.SetActive(true);
                
                FillSolutionEventPanel((ChoiceEvent)_event);
            }
    
            panelTransform.anchoredPosition = new Vector2(0, -1000);

            var finishPosition = new Vector2(-300, 0);
            
            panelTransform.DOAnchorPos(finishPosition, 0.5f)
                .SetEase(Ease.OutBack);
        }

        private void FillSolutionEventPanel(ChoiceEvent choiceEvent) => 
            choiceEventPanel.SetText(choiceEvent.eventName, choiceEvent.description, 
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

