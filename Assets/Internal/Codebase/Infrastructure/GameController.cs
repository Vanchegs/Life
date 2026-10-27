using UnityEngine;
using UnityEngine.SceneManagement;

namespace Codebase
{
    public class GameController : MonoBehaviour
    {
        public static LossGameTypes LossType { get; private set; }

        private bool isGameOver;

        private void OnEnable() =>
            GameEventBus.OnLossGame += FinishGame;

        private void OnDisable() =>
            GameEventBus.OnLossGame -= FinishGame;

        private void FinishGame(LossGameTypes lossType)
        {
            if (isGameOver)
                return;

            isGameOver = true;
            LossType = lossType;
            GameEventBus.RaiseSaveGame();
            SceneManager.LoadScene("FinalScene");
        }
    }
}
