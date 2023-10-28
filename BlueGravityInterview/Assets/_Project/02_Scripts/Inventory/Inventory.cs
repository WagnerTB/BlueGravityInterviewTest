using System;
using System.Collections.Generic;
using UnityEngine;
using Items;

namespace InventorySystem
{
    public class Inventory : MonoBehaviour
    {
        public List<Item> Items => _items;

        [SerializeField]
        private int _inventorySize = 10;

        [SerializeField]
        protected List<Item> _items = new List<Item>();

        public Action<Item, int> OnInventoryUpdate;

        private void Awake()
        {
            int difference = _inventorySize - _items.Count;

            if (difference > 0)
            {
                for (int i = 0; i < difference; i++)
                {
                    _items.Add(null);
                }
            }
        }

        public bool AddItem(Item item)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                var itemSlot = _items[i];
                if (itemSlot == null)
                {
                    _items[i] = item;
                    OnInventoryUpdate?.Invoke(item, i);
                    return true;
                }
            }

            return false;
        }

        public void SetItemAtIndex(Item item, int index)
        {
            if (index >= _inventorySize)
            {
                Debug.LogError($"Index {index} don't exists!");
                return;
            }

            _items[index] = item;
            OnInventoryUpdate?.Invoke(item, index);
        }

        public void RemoveItem(Item item)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                var itemTarget = _items[i];
                if (itemTarget == null) continue;

                if (itemTarget == item)
                {
                    _items.RemoveAt(i);
                    OnInventoryUpdate?.Invoke(item, i);
                }
            }
        }

        public bool HasItem(Item item)
        {
            return _items.Exists(x => x == item);
        }
    }
}