using System;
using TMPro;
using UnityEngine;

namespace Codebase
{
    [Serializable]
    public class SolutionEventPanel
    {
        [SerializeField] private TMP_Text eventNameText;
        [SerializeField] private TMP_Text eventDescriptionText;
        [SerializeField] private TMP_Text firstSolutionText;
        [SerializeField] private TMP_Text secondSolutionText;

        public void SetText(string nameText, string descriptionText, 
            string firstSolutionText, string secondSolutionText)
        {
            eventNameText.text = nameText;
            eventDescriptionText.text = descriptionText;
            this.firstSolutionText.text = firstSolutionText;
            this.secondSolutionText.text = secondSolutionText;
        }
    }
}