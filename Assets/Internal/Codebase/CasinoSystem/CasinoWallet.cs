using UnityEngine;

namespace Codebase
{
    public class CasinoWallet
    {
        public int Balance { get; private set; }

        public void GetSavedBalance()
        {
            var savedBalance = PlayerDataSave.Load<SaveData>() ?? new SaveData();

            Balance = Mathf.Max(0, savedBalance.Balance);
            
            Debug.Log(Balance);
        }

        public void SaveBalance()
        {
            var savedBalance = PlayerDataSave.Load<SaveData>() ?? new SaveData();

            savedBalance.Balance = Balance;
            
            PlayerDataSave.Save(savedBalance);
        }

        public void IncreaseBalance(int changeValue)
        {
            Balance += changeValue;
            
            SaveBalance();
        }

        public void DecreaseBalance(int changeValue)
        {
            Balance -= changeValue;
            
            SaveBalance();
        }
    }
}
