using System;
using Items;
using UI;
using UnityEngine;

namespace InventorySystem
{
    public class EquipmentInventory : Inventory
    {
        [Header("Default Items")]
        [SerializeField]
        private EquipmentItem _defaultHead;

        [SerializeField]
        private EquipmentItem _defaultBody;

        [SerializeField]
        private EquipmentItem _defaultLeg;

        [SerializeField]
        private EquipmentItem _headEquipment;

        [SerializeField]
        private EquipmentItem _bodyEquipment;

        [SerializeField]
        private EquipmentItem _legEquipment;

        public Action<EquipmentItem, ItemSlotType> OnEquipmentChange;

        private void Start()
        {
            OnEquipmentChange?.Invoke(_headEquipment, ItemSlotType.Head);
            OnEquipmentChange?.Invoke(_bodyEquipment, ItemSlotType.Body);
            OnEquipmentChange?.Invoke(_legEquipment, ItemSlotType.Leg);

            for (int i = 0; i < _items.Count; i++)
            {
                var item = _items[i];
                if (item != null)
                    OnInventoryUpdate?.Invoke(item, i);
            }
        }

        public void ChangeEquipItem(ItemSlot itemSlot)
        {
            if (itemSlot.Item == null) return;

            EquipItem((EquipmentItem)itemSlot.Item, itemSlot.Index);
        }

        public void UnEquipItem(ItemSlot itemSlot)
        {
            if (itemSlot.Item == null) return;
            var equip = (EquipmentItem)itemSlot.Item;
            if (equip.IsDefaultItem) return;

            RemoveItem((EquipmentItem)itemSlot.Item, itemSlot.Index);
        }

        private void EquipItem(EquipmentItem item, int inventorySlotIndex)
        {
            switch (item.ItemSlotType)
            {
                case ItemSlotType.None:
                    Debug.LogError($"Item {item.ItemName} has no slot type defined!!");
                    return;
                case ItemSlotType.Head:
                {
                    if (_headEquipment != null)
                    {
                        if (!_headEquipment.IsDefaultItem)
                            SetItemAtIndex(_headEquipment, inventorySlotIndex);
                        else
                            SetItemAtIndex(null, inventorySlotIndex);

                        _headEquipment = item;
                    }
                    else
                    {
                        _headEquipment = item;
                    }
                }
                    break;
                case ItemSlotType.Body:
                {
                    if (_bodyEquipment != null)
                    {
                        if (!_bodyEquipment.IsDefaultItem)
                            SetItemAtIndex(_bodyEquipment, inventorySlotIndex);
                        else
                            SetItemAtIndex(null, inventorySlotIndex);
                        _bodyEquipment = item;
                    }
                    else
                    {
                        _bodyEquipment = item;
                    }
                }
                    break;
                case ItemSlotType.Leg:
                {
                    if (_legEquipment != null)
                    {
                        if (!_legEquipment.IsDefaultItem)
                            SetItemAtIndex(_legEquipment, inventorySlotIndex);
                        else
                            SetItemAtIndex(null, inventorySlotIndex);
                        _legEquipment = item;
                    }
                    else
                    {
                        _legEquipment = item;
                    }
                }
                    break;
            }

            OnEquipmentChange?.Invoke(item, item.ItemSlotType);
        }

        private void RemoveItem(EquipmentItem item, int inventorySlotIndex)
        {
            switch (item.ItemSlotType)
            {
                case ItemSlotType.None:
                    Debug.LogError($"Item {item.ItemName} has no slot type defined!!");
                    return;
                case ItemSlotType.Head:
                {
                    if (_headEquipment != null)
                    {
                        AddItem(_headEquipment);
                        item = _defaultHead;
                        _headEquipment = _defaultHead;
                    }
                }
                    break;
                case ItemSlotType.Body:
                {
                    if (_bodyEquipment != null)
                    {
                        AddItem(_bodyEquipment);
                        item = _defaultBody;
                        _bodyEquipment = _defaultBody;
                    }
                }
                    break;
                case ItemSlotType.Leg:
                {
                    if (_legEquipment != null)
                    {
                        AddItem(_legEquipment);
                        item = _defaultLeg;
                        _legEquipment = _defaultLeg;
                    }
                }
                    break;
            }

            OnEquipmentChange?.Invoke(item, item.ItemSlotType);
        }
    }
}