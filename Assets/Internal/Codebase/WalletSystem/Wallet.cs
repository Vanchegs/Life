using UnityEngine;

namespace Codebase
{
    public class Wallet
    {
        private int balance;

        public void ChangeBalance(int changeValue)
        {
            balance += changeValue;
            Debug.Log(balance);
        }

        public void SetSaveBalance(int savedBalance) => 
            balance = savedBalance;
    }
}

