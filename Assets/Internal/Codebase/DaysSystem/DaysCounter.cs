using System.Collections;
using UnityEngine;

namespace Codebase
{
    public class DaysCounter : MonoBehaviour
    {
        private int dayNumber = 1;

        public int DayNumber => dayNumber;

        private void Start()
        {
            StartCoroutine(ChangeDays());
            
            GameEventBus.RaiseUpdateDayNumber(dayNumber);
        }

        private void IncreaseDayNumber()
        {
            dayNumber++;
            
            GameEventBus.RaiseSaveGame();
        }

        public void SetSavedDay(int savedDayNumber)
        {
            dayNumber = Mathf.Max(1, savedDayNumber);
            GameEventBus.RaiseUpdateDayNumber(dayNumber);
        }

        private IEnumerator ChangeDays()
        {
            while (true)
            {
                yield return new WaitForSeconds(15);
                            
                IncreaseDayNumber();
                            
                GameEventBus.RaiseUpdateDayNumber(dayNumber);
            }
        }
    }
}

