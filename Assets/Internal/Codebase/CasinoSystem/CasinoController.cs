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
        private bool hasSelectedColor;

        private enum BetColors
        {
            Red,
            Black,
            Green
        }

        private void Awake()
        {
            casinoWallet = new CasinoWallet();
            
            casinoWallet.GetSavedBalance();
        }

        private void OnEnable()
        {
            GameEventBus.RaiseUpdateCasinoBalance(casinoWallet.Balance);
            GameEventBus.RaiseUpdateBetValueChange(betAmount);
        }

        private void OnDisable() => 
            casinoWallet?.SaveBalance();

        public void BetColorClick(int colorIndex)
        {
            if (colorIndex < 0 || colorIndex > 2)
                return;

            betColor = colorIndex switch
            {
                0 => BetColors.Green,
                1 => BetColors.Black,
                2 => BetColors.Red,
                _ => betColor
            };

            hasSelectedColor = true;
            
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

            if (!hasSelectedColor)
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
                Debug.Log($"ПОБЕДА! Выигрыш: {winAmount}, Баланс: {casinoWallet.Balance}");
            }
            else
            {
                Debug.Log($"ПРОИГРЫШ! Баланс: {casinoWallet.Balance}");
            }
            
            GameEventBus.RaiseUpdateCasinoBalance(casinoWallet.Balance);
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
            GameEventBus.RaiseUpdateBetValueChange(betAmount);
        }
        
        public void DecreaseBet(int decrement = 10)
        {
            betAmount = Mathf.Max(10, betAmount - decrement);
            GameEventBus.RaiseUpdateBetValueChange(betAmount);
        }
    }
}
