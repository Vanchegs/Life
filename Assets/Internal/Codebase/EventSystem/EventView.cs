using TMPro;
using UnityEngine;

namespace Codebase
{
    public class EventView : MonoBehaviour
    {
        [SerializeField] private GameObject eventPanel;
        
        [SerializeField] private TMP_Text eventNameText;
        [SerializeField] private TMP_Text eventDescriptionText;
        [SerializeField] private TMP_Text firstSolutionText;
        [SerializeField] private TMP_Text secondSolutionText;

        public void ShowEvent(Event _event)
        {
            eventPanel.SetActive(true);
            
            eventNameText.text = _event.eventName;
            eventDescriptionText.text = _event.description;
            firstSolutionText.text = _event.firstSolution;
            secondSolutionText.text = _event.secondSolution;
        }
    }
}

