using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase
{
    public class EventView : MonoBehaviour
    {
        [SerializeField] private TMP_Text eventNameText;
        [SerializeField] private TMP_Text eventDescriptionText;

        [SerializeField] private Button firstSolutionButton;
        [SerializeField] private Button secondSolutionButton;

        public void ViewEvent(Event _event)
        {
            eventNameText.text = _event.eventName;
            eventDescriptionText.text = _event.description;
        }
    }
}

