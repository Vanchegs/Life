using System;
using UnityEngine;

namespace Codebase
{
    public class StateController : MonoBehaviour
    {
        private StatsController statsController;

        public void Update()
        {
            CheckInput();
        }

        private void CheckInput()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
            {
                Debug.Log("Нажата клавиша 1 - Спать");
            }
        
            if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
            {
                Debug.Log("Нажата клавиша 2 - Работать");
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
            {
            }

            switch (typeof(KeyCode))
            {
                
            }
        }
    }
}

