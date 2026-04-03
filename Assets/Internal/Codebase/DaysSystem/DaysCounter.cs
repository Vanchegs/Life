using UnityEngine;

namespace Codebase
{
    public class DaysCounter : MonoBehaviour
    {
        private int dayNumber;

        private void Start()
        {
            GetSavedDay();
        }

        private void IncreaseDayNumber() => 
            dayNumber++;

        private void GetSavedDay()
        {
            var savedDayNumber = PlayerDataSave.Load<SaveData>();

            dayNumber = savedDayNumber.DayNumber;
        }
    }
}

