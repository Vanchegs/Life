using UnityEngine;

namespace Codebase
{
    public class CharacterView : MonoBehaviour
    {
        [SerializeField] private GameObject workCharacter;
        [SerializeField] private GameObject gamingCharacter;
        [SerializeField] private GameObject eatCharacter;
        [SerializeField] private GameObject sleepCharacter;

        public void ShowWork() =>
            ShowCharacter(workCharacter);

        public void ShowGaming() =>
            ShowCharacter(gamingCharacter);

        public void ShowEat() =>
            ShowCharacter(eatCharacter);

        public void ShowSleep() =>
            ShowCharacter(sleepCharacter);

        public void HideCharacters() =>
            ShowCharacter(null);

        private void ShowCharacter(GameObject character)
        {
            workCharacter.SetActive(character == workCharacter);
            gamingCharacter.SetActive(character == gamingCharacter);
            eatCharacter.SetActive(character == eatCharacter);
            sleepCharacter.SetActive(character == sleepCharacter);
        }
    }
}
