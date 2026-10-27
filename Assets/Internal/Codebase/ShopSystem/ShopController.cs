using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Codebase
{
    public class ShopController : MonoBehaviour
    {
        [SerializeField] private PanelSlider shopView;
        [SerializeField] private StatsController statsController;
        [SerializeField] private WalletController walletController;

        [SerializeField, SerializedDictionary] private SerializedDictionary<ItemType, ShopItem> shopItemSlots;

        /*public void OpenCloseShop() => 
            shopView.MoveShop();*/

        public void BuyClick(ShopItem shopItem)
        {
            if (shopItem == null || statsController.IsGameOver)
                return;

            if (!walletController.GetWallet().TrySpend(shopItem.price))
                return;

            statsController.ChangeStats(
                moneyChangeValue: 0,
                foodChangeValue: shopItem.foodChangeValue,
                energyChangeValue: shopItem.energyChangeValue,
                mentalHealthChangeValue: shopItem.mentalChangeValue,
                healthChangeValue: shopItem.healthChangeValue);
        }
    }
}

