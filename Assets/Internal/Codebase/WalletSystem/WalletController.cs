using UnityEngine;

namespace Codebase
{
    public class WalletController : MonoBehaviour
    {
        private Wallet wallet;

        private void Awake()
        {
            wallet = new Wallet();
            GameEventBus.OnUpdateBalance?.Invoke(wallet.GetBalance());
        }
        
        public Wallet GetWallet() => 
            wallet;
        
        public void IncreaseWalletBalance() => 
            wallet.ChangeBalance(15);
    }
}