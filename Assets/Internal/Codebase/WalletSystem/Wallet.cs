namespace Codebase
{
    public class Wallet
    {
        private int balance;
        
        public void ChangeBalance(int changeValue)
        {
            balance += changeValue;
            GameEventBus.OnUpdateBalance?.Invoke(balance);
        }

        public int GetBalance() => 
            balance;

        public void SetSaveBalance(int savedBalance)
        {
            balance = savedBalance;
            GameEventBus.OnUpdateBalance?.Invoke(balance);
        }
    }
}

