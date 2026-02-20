using UnityEngine;

namespace Codebase
{
    [CreateAssetMenu(menuName = "Configs/Shop Item Config", fileName = "ShopItem")]
    public class ShopItem : ScriptableObject
    {
        public int price;

        [Space(10)]
        public int healthChangeValue;
        public int mentalChangeValue;
        public int foodChangeValue;
        public int energyChangeValue;
    }
}

