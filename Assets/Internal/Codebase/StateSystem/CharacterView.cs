using UnityEngine;

namespace Codebase
{
    public class CharacterView : MonoBehaviour
    {
        [SerializeField] private GameObject workCharacter;
        [SerializeField] private GameObject gamingCharacter;
        [SerializeField] private GameObject eatCharacter;
        [SerializeField] private GameObject sleepCharacter;

        [SerializeField] private GameObject workFurniture;
        [SerializeField] private GameObject gamingFurniture;
        [SerializeField] private GameObject sleepFurniture;

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
            workFurniture.SetActive(character != workCharacter);
            gamingFurniture.SetActive(character != gamingCharacter);
            sleepFurniture.SetActive(character != sleepCharacter);

            workCharacter.SetActive(character == workCharacter);
            gamingCharacter.SetActive(character == gamingCharacter);
            eatCharacter.SetActive(character == eatCharacter);
            sleepCharacter.SetActive(character == sleepCharacter);
        }
    }
}
