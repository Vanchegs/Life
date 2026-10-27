using System;

namespace Codebase
{
    public class Wallet
    {
        private int balance;
        
        public void ChangeBalance(int changeValue)
        {
            balance = (int)Math.Max(0, Math.Min(int.MaxValue, (long)balance + changeValue));
            GameEventBus.RaiseUpdateBalance(balance);
        }

        public int GetBalance() => 
            balance;

        public bool TrySpend(int amount)
        {
            if (amount < 0 || balance < amount)
                return false;

            ChangeBalance(-amount);
            return true;
        }

        public void SetSaveBalance(int savedBalance)
        {
            balance = Math.Max(0, savedBalance);
            GameEventBus.RaiseUpdateBalance(balance);
        }
    }
}

