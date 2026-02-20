using UnityEngine;

namespace Codebase
{
    public class ShopController : MonoBehaviour
    {
        [SerializeField] private ShopView shopView;

        [SerializeField] private ShopItemSlot shopItemSlot;

        public void OpenCloseShop() => 
            shopView.MoveStore();
    }
}

