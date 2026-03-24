using UnityEngine;

namespace Codebase
{
    public class CasinoController : MonoBehaviour
    {
        private BetColors betColor;

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
            var colorIndex = Random.Range(0, 3);
            
            
        }
    }
}

