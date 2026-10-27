using TMPro;
using UnityEngine;

namespace Codebase
{
   public class CasinoWalletView : MonoBehaviour
   {
       [SerializeField] private TMP_Text balanceView;

       private void OnEnable()
       {
           GameEventBus.OnUpdateCasinoBalance += UpdateBalanceView;
           UpdateBalanceView(GameEventBus.CasinoBalance);
       }

       private void OnDisable() => 
           GameEventBus.OnUpdateCasinoBalance -= UpdateBalanceView;

       private void UpdateBalanceView(int balance) => 
           balanceView.text = balance.ToString();
   } 
}

