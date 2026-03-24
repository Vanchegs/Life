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

        private BetColors betColor;
        private int betAmount = 20;
        private int balance;

        private enum BetColors
        {
            Red,
            Black,
            Green
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
        }

        public void SpinClick()
        {
            if (betAmount < 10)
                return;
            
            var winColorIndex = Random.Range(0, 3); //переделать под малоый шанс выпадения зеленого
            
            var winBetColor = winColorIndex switch
            {
                0 => BetColors.Green,
                1 => BetColors.Black,
                2 => BetColors.Red,
                _ => betColor
            };

            if (winBetColor != betColor) return;
            
            switch (winBetColor)
            {
                case BetColors.Black:
                    balance += betAmount * BlackMultiplier;
                    break;
                case BetColors.Red:
                    balance += betAmount *= RedMultiplier;
                    break;
                case BetColors.Green:
                    balance += betAmount *= GreenMultiplier;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
                
            Debug.Log(balance);
        }
    }
}

