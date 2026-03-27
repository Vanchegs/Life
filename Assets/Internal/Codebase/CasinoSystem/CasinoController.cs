using UnityEngine;
using Random = UnityEngine.Random;

namespace Codebase
{
    public class CasinoController : MonoBehaviour
    {
        private const int RedMultiplier = 2;
        private const int BlackMultiplier = 2;
        private const int GreenMultiplier = 10;

        private const float GreenChance = 0.05f;
        private const float RedChance = 0.475f;

        private BetColors betColor;
        private int betAmount = 10;
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
            
            GameEventBus.OnUpdateCasinoBalance?.Invoke(casinoWallet.Balance);
            GameEventBus.OnUpdateBetValueChange?.Invoke(betAmount);
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

            if (casinoWallet.Balance < betAmount)
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
            
            casinoWallet.DecreaseBalance(betAmount);
            
            if (winColor == betColor)
            {
                var winAmount = CalculateWinAmount(winColor);
                casinoWallet.IncreaseBalance(winAmount);
                GameEventBus.OnUpdateCasinoBalance?.Invoke(casinoWallet.Balance);
                Debug.Log($"ПОБЕДА! Выигрыш: {winAmount}, Баланс: {casinoWallet.Balance}");
            }
            else
            {
                Debug.Log($"ПРОИГРЫШ! Баланс: {casinoWallet.Balance}");
                GameEventBus.OnUpdateCasinoBalance?.Invoke(casinoWallet.Balance);
            }
            
            GameEventBus.OnUpdateCasinoBalance?.Invoke(casinoWallet.Balance);
        }
        
        private BetColors GetRandomColor()
        {
            var randomValue = Random.Range(0f, 1f);

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
        
        public void IncreaseBet(int increment = 10)
        {
            betAmount += increment;
            GameEventBus.OnUpdateBetValueChange?.Invoke(betAmount);
            Debug.Log($"Ставка увеличена: {betAmount}");
        }
        
        public void DecreaseBet(int decrement = 10)
        {
            betAmount = Mathf.Max(10, betAmount - decrement);
            GameEventBus.OnUpdateBetValueChange?.Invoke(betAmount);
            Debug.Log($"Ставка уменьшена: {betAmount}");
        }
    }
}