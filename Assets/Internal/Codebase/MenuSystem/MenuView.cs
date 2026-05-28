using DG.Tweening;
using UnityEngine;

namespace Codebase
{
    public class MenuView : MonoBehaviour
    {
        private bool isStoreActivate;
        
        [SerializeField] private RectTransform startPosition;
        [SerializeField] private RectTransform finalPosition;
        [SerializeField] private RectTransform menuTransform;
        
        public void MoveShop()
        {
            if (isStoreActivate == false)
            {
                menuTransform.DOMoveX(finalPosition.position.x, 0.5f, false);
                isStoreActivate = true;
            }
            else
            {
                menuTransform.DOMoveX(startPosition.position.x, 0.5f, false);
                isStoreActivate = false;
            }
        }
    }
}

