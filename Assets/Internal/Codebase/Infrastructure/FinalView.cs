using UnityEngine;

namespace Codebase
{
    public class FinalView : MonoBehaviour
    {
        [SerializeField] private GameObject exhaustionCharacter;
        [SerializeField] private GameObject suicideCharacter;

        private void Start()
        {
            exhaustionCharacter.SetActive(GameController.LossType == LossGameTypes.Exhaustion);
            suicideCharacter.SetActive(GameController.LossType == LossGameTypes.Suicide);
        }
    }
}
