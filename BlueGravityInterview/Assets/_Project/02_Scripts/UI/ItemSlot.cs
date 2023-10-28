using System;
using Items;
using Player;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class ItemSlot : MonoBehaviour
    {
        public Item Item => _item;
        public int Index => _index;

        [SerializeField]
        private Image _itemImage;
    
        [SerializeField]
        private Button _button;
    
        [SerializeField]
        private Item _item;

        [SerializeField]
        private UnityEvent<ItemSlot> _onClick;

        private int _index;

        private void Awake()
        {
            _button.onClick.AddListener(OnClickSlot);
        }

        private void OnClickSlot()
        {
            _onClick?.Invoke(this);
        }

        public void SetItem(Item item)
        {
            if (item != null)
            {
                _item = item;
                _itemImage.sprite = item.Sprite;
                Color color = Color.white;
                color.a = 1;
                _itemImage.color = color;
            }
            else
            {
                _item = null;
                _itemImage.sprite = null;
                Color color = Color.white;
                color.a = 0;
                _itemImage.color = color;
            }
        }

        public void RemoveItem()
        {
            if(_item == null)
                return;

            _item = null;
            _itemImage.sprite = null;
        
            Color color = Color.white;
            color.a = 0;
        
            _itemImage.color = color;
        }

        public void UseItem()
        {
            if (_item != null)
            {
                switch (_item.Type)
                {
                    case ItemType.Common:
                        break;
                    case ItemType.Currency:
                        break;
                    case ItemType.Equipment:
                        PlayerController.Instance.EquipmentInventory.ChangeEquipItem(this);
                        break;
                }
            }
        }
        
        
        public void SetIndex(int i)
        {
            _index = i;
        }
    }
}