using UnityEngine;

namespace Codebase
{
    public class CasinoWallet
    {
        public int Balance { get; private set; }

        public void GetSavedBalance()
        {
            var savedBalance = PlayerDataSave.Load<SaveData>();

            Balance = savedBalance.Balance;
            
            Debug.Log(Balance);
        }

        public void SaveBalance()
        {
            var savedBalance = PlayerDataSave.Load<SaveData>();

            savedBalance.Balance = Balance;
            
            PlayerDataSave.Save(savedBalance);
        }

        public void IncreaseBalance(int changeValue) => 
            Balance += changeValue;

        public void DecreaseBalance(int changeValue) => 
            Balance -= changeValue;
    }
}