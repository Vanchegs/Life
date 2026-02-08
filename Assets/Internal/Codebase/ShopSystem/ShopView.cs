using DG.Tweening;
using UnityEngine;

namespace Codebase
{
    public class ShopView : MonoBehaviour
    {
        private bool isStoreActivate;

        [SerializeField] private RectTransform startPosition;
        [SerializeField] private RectTransform finalPosition;

        public void MoveStore()
        {
            if (isStoreActivate == false)
            {
                transform.DOMoveX(finalPosition.position.x, 1.5f, false);
                isStoreActivate = true;
            }
            else
            {
                transform.DOMoveX(startPosition.position.x, 1.5f, false);
                isStoreActivate = false;
            }
        }
    }
}
