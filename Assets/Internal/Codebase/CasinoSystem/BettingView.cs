using TMPro;
using UnityEngine;

namespace Codebase
{
    public class BettingView : MonoBehaviour
    {
        [SerializeField] private TMP_Text betValue;

        private void Start() => 
            GameEventBus.OnUpdateBetValueChange += ChangeBetValue;

        private void OnDisable() => 
            GameEventBus.OnUpdateBetValueChange -= ChangeBetValue;

        private void ChangeBetValue(int betValue) => 
            this.betValue.text = betValue.ToString();
    }
}