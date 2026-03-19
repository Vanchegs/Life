using System;
using TMPro;
using UnityEngine;

namespace Codebase
{
    [Serializable]
    public class SimpleEventPanel
    {
        [SerializeField] private TMP_Text eventNameText;
        [SerializeField] private TMP_Text eventDescriptionText;
        [SerializeField] private TMP_Text firstSolutionText;

        public void SetText(string nameText, string descriptionText, string firstSolutionText)
        {
            eventNameText.text = nameText;
            eventDescriptionText.text = descriptionText;
            this.firstSolutionText.text = firstSolutionText;
        }
    }
}