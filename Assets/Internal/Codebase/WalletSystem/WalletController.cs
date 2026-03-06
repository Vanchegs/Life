using UnityEngine;

namespace Codebase
{
    public class WalletController : MonoBehaviour
    {
        private Wallet wallet;

        private void Start()
        {
            wallet = new Wallet();
            GameEventBus.OnUpdateBalance?.Invoke(wallet.GetBalance());
        }
        
        public Wallet GetWallet() => 
            wallet;
        
        public void IncreaseWalletBalance() => 
            wallet.ChangeBalance(2);
    }
}