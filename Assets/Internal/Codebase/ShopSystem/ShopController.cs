using System.Collections.Generic;
using UnityEngine;

namespace Codebase
{
    public class ShopController : MonoBehaviour
    {
        [SerializeField] private ShopView shopView;

        [SerializeField] private List<ShopItemSlot> itemSlots;

        public void OpenCloseShop() => 
            shopView.MoveStore();
    }
}

