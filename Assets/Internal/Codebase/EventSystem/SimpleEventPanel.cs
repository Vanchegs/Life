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
        [SerializeField] private TMP_Text solutionText;

        public void SetText(string nameText, string descriptionText, string solutionText)
        {
            eventNameText.text = nameText;
            eventDescriptionText.text = descriptionText;
            this.solutionText.text = solutionText;
        }
    }
}