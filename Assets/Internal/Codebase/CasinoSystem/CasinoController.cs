using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Codebase
{
    public class CasinoController : MonoBehaviour
    {
        private const int RedMultiplier = 2;
        private const int BlackMultiplier = 2;
        private const int GreenMultiplier = 10;

        private const float GreenChance = 0.1f;
        private const float RedChance = 0.45f;
        private const float BlackChance = 0.45f;

        private BetColors betColor;
        private int betAmount = 20;
        private CasinoWallet casinoWallet;

        private enum BetColors
        {
            Red,
            Black,
            Green
        }

        private void Start()
        {
            casinoWallet = new CasinoWallet();
            
            casinoWallet.GetSavedBalance();
        }

        public void BetColorClick(int colorIndex)
        {
            betColor = colorIndex switch
            {
                0 => BetColors.Green,
                1 => BetColors.Black,
                2 => BetColors.Red,
                _ => betColor
            };
            
            Debug.Log($"Выбрана ставка на: {betColor}");
        }

        public void SpinClick()
        {
            if (betAmount < 10)
            {
                Debug.Log("Минимальная ставка: 10");
                return;
            }

            if (casinoWallet.Balance <= 0)
            {
                Debug.Log("Недостаточно денег");
                return;
            }

            if (betColor == BetColors.Red && betAmount == 0)
            {
                Debug.Log("Выберите цвет для ставки!");
                return;
            }

            BetColors winColor = GetRandomColor();
            
            Debug.Log($"Выпало: {winColor}, Ваша ставка: {betColor}");
            
            if (winColor != betColor)
            {
                Debug.Log($"Проигрыш! Ставка {betAmount} сгорела");
                casinoWallet.DecreaseBalance(betAmount);
                return;
            }
            
            int winAmount = CalculateWinAmount(winColor);
            casinoWallet.IncreaseBalance(winAmount);
            
            Debug.Log($"ПОБЕДА! Выигрыш: {winAmount}, Баланс: {casinoWallet.Balance}");
        }
        
        private BetColors GetRandomColor()
        {
            float randomValue = Random.Range(0f, 1f);

            return randomValue switch
            {
                < GreenChance => BetColors.Green,
                < GreenChance + RedChance => BetColors.Red,
                _ => BetColors.Black
            };
        }
        
        private int CalculateWinAmount(BetColors color)
        {
            return color switch
            {
                BetColors.Red => betAmount * RedMultiplier,
                BetColors.Black => betAmount * BlackMultiplier,
                BetColors.Green => betAmount * GreenMultiplier,
                _ => 0
            };
        }
        
        public int GetBalance() => casinoWallet.Balance;
        
        public int GetBetAmount() => betAmount;
        
        public void SetBetAmount(int amount)
        {
            betAmount = Mathf.Max(10, amount);
            Debug.Log($"Ставка изменена: {betAmount}");
        }
        
        public void IncreaseBet(int increment = 10)
        {
            betAmount += increment;
            Debug.Log($"Ставка увеличена: {betAmount}");
        }
        
        public void DecreaseBet(int decrement = 10)
        {
            betAmount = Mathf.Max(10, betAmount - decrement);
            Debug.Log($"Ставка уменьшена: {betAmount}");
        }
    }
}