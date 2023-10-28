using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;
using InventorySystem;
using Items;

namespace UI
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroupFade _canvasGroupFade;

        [SerializeField]
        private Camera _playerRenderCamera;

        [SerializeField]
        private ItemSlot _headEquipmentSlot;

        [SerializeField]
        private ItemSlot _bodyEquipmentSlot;

        [SerializeField]
        private ItemSlot _legEquipmentSlot;

        [SerializeField]
        private EquipmentInventory _inventory;

        [SerializeField]
        private Transform _playerTransform;

        [SerializeField]
        private Vector3 _cameraOffset;

        [SerializeField]
        private List<ItemSlot> _itemSlots = new List<ItemSlot>();

        private bool _isShowing = false;

        private void Awake()
        {
            _inventory.OnEquipmentChange += OnEquipmentChange;
            _inventory.OnInventoryUpdate += OnInventoryUpdate;
            _playerRenderCamera.transform.SetParent(_playerTransform);
            _playerRenderCamera.transform.position = _cameraOffset;

            for (int i = 0; i < _itemSlots.Count; i++)
            {
                _itemSlots[i].SetIndex(i);
            }
        }

        private void OnDestroy()
        {
            _inventory.OnEquipmentChange -= OnEquipmentChange;
            _inventory.OnInventoryUpdate -= OnInventoryUpdate;
        }

        private void OnInventoryUpdate(Item item, int index)
        {
            _itemSlots[index].SetItem(item);
        }

        private void OnEquipmentChange(EquipmentItem item, ItemSlotType itemSlotType)
        {
            ItemSlotType slotType = item != null ? item.ItemSlotType : itemSlotType;
            switch (slotType)
            {
                case ItemSlotType.None:
                    Debug.LogError($"Tried to add item with slot type as none! => {item.ItemName}");
                    return;
                case ItemSlotType.Head:
                    _headEquipmentSlot.SetItem(item);
                    break;
                case ItemSlotType.Body:
                    _bodyEquipmentSlot.SetItem(item);
                    break;
                case ItemSlotType.Leg:
                    _legEquipmentSlot.SetItem(item);
                    break;
            }
        }

        public bool ToggleInventory()
        {
            _isShowing = !_isShowing;
            _playerRenderCamera.enabled = _isShowing;
            if (_isShowing)
                _canvasGroupFade.FadeIn();
            else
                _canvasGroupFade.FadeOut();

            return _isShowing;
        }
    }
}