using Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ShopItemSlot : MonoBehaviour
    {
        public TradableItem Item => _item;
        public Button Button => _button;

        [SerializeField]
        private TradableItem _item;

        [SerializeField]
        private Image _itemIcon;

        [SerializeField]
        private TextMeshProUGUI _itemNameText;

        [SerializeField]
        private TextMeshProUGUI _itemCostText;

        [SerializeField]
        private Button _button;

        [SerializeField]
        private bool _isPlayerItem = false;

        public void LoadItem(TradableItem item, bool isPlayerItem)
        {
            if (item == null) return;
            _item = item;

            _itemIcon.sprite = _item.Sprite;
            _itemNameText.text = _item.ItemName;
            _itemCostText.text = isPlayerItem ? _item.SellAmount.ToString() : _item.BuyAmount.ToString();
            _isPlayerItem = isPlayerItem;
        }
    }
}