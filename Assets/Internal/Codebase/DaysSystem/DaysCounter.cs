using System.Collections;
using UnityEngine;

namespace Codebase
{
    public class DaysCounter : MonoBehaviour
    {
        private int dayNumber;

        public int DayNumber => dayNumber;

        private void Start()
        {
            GetSavedDay();

            StartCoroutine(ChangeDays());
            
            GameEventBus.OnUpdateDayNumber.Invoke(dayNumber);
        }

        private void IncreaseDayNumber()
        {
            dayNumber++;
            
            GameEventBus.OnSaveGame?.Invoke();
        }

        private void GetSavedDay()
        {
            var savedDayNumber = PlayerDataSave.Load<SaveData>();

            dayNumber = savedDayNumber?.DayNumber ?? 1;
        }

        private IEnumerator ChangeDays()
        {
            while (true)
            {
                yield return new WaitForSeconds(15);
                            
                IncreaseDayNumber();
                            
                GameEventBus.OnUpdateDayNumber?.Invoke(dayNumber);
            }
        }
    }
}

