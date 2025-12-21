using UnityEngine;

namespace Codebase
{
    public class StateController : MonoBehaviour
    {
        [SerializeField] private StatsController statsController;

        private void Update()
        {
            CheckInput();
        }

        private void CheckInput()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                statsController.IncreaseCurrentStat(StatType.FoodStat);
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                statsController.IncreaseCurrentStat(StatType.HealthStat);
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                statsController.IncreaseCurrentStat(StatType.MentalHealthStat);
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                statsController.IncreaseCurrentStat(StatType.SleepStat);
            }
        }

        private void UpdateIncreaseStat()
        {
            
        }
    }
}

