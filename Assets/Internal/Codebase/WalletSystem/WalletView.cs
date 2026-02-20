using TMPro;
using UnityEngine;

namespace Codebase
{
    public class WalletView : MonoBehaviour
    {
        [SerializeField] private TMP_Text balanceView;

        private void Start() => 
            GameEventBus.OnUpdateBalance += UpdateBalanceView;

        private void OnDisable() => 
            GameEventBus.OnUpdateBalance -= UpdateBalanceView;

        private void UpdateBalanceView(int balance)
        {
            balanceView.text = balance.ToString();
        }
    }
}

