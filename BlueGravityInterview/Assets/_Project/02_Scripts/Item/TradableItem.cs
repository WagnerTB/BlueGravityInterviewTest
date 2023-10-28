using UnityEngine;

namespace Items
{
    public class TradableItem : Item
    {
        [Header("Trade Settings")]
        public int BuyAmount;

        public int SellAmount;
    }
}