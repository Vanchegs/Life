using UnityEngine;

namespace Codebase
{
    public class ShopController : MonoBehaviour
    {
        [SerializeField] private ShopView shopView;

        public void OpenCloseShop() => 
            shopView.MoveStore();
    }
}

