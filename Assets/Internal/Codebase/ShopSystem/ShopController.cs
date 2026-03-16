using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Codebase
{
    public class ShopController : MonoBehaviour
    {
        [SerializeField] private ShopView shopView;

        [SerializeField, SerializedDictionary] private SerializedDictionary<ItemType, ShopItem> shopItemSlots;

        public void OpenCloseShop() => 
            shopView.MoveStore();

        public void BuyButton(ShopItem shopItem)
        {
            
        }
    }
}

