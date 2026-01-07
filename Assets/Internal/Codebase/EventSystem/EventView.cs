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
    }
}

