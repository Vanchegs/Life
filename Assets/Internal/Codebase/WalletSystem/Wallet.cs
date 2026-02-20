namespace Codebase
{
    public class Wallet
    {
        private int balance;

        public void ChangeBalance(int changeValue) => 
            balance += changeValue;

        public void SetSaveBalance(int savedBalance) => 
            balance = savedBalance;
    }
}

