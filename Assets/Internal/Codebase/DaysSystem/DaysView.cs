using System;
using TMPro;
using UnityEngine;

namespace Codebase
{
    public class DaysView : MonoBehaviour
    {
        [SerializeField] private TMP_Text daysText;

        private void OnEnable() => 
            GameEventBus.OnUpdateDayNumber += UpdateDayNumber;

        private void OnDisable() => 
            GameEventBus.OnUpdateDayNumber -= UpdateDayNumber;

        private void UpdateDayNumber(int number) => 
            daysText.text = number.ToString();
    }
}

