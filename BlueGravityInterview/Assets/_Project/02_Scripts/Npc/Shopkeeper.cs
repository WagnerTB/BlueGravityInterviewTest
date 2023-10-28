using System.Collections.Generic;
using Currency;
using Interaction;
using InventorySystem;
using Items;
using Player;
using UI;
using UnityEngine;
using Util;

namespace Npc
{
    public class Shopkeeper : NPC , IInteractable
    {
        [SerializeField]
        private Inventory _inventory;

        [SerializeField]
        private Transform _npcItemsParent;
        [SerializeField]
        private Transform _playerItemsParent;

        [SerializeField]
        private ShopItemSlot _shopItemPrefab;

        [SerializeField]
        private CanvasGroupFade _canvasGroupFade;
        [SerializeField]
        private List<ShopItemSlot> _ncpShopItems = new List<ShopItemSlot>();
        [SerializeField]
        private List<ShopItemSlot> _playerShopItems = new List<ShopItemSlot>();
        
        public void BeginInteract()
        {
            for (int i = 0; i < _ncpShopItems.Count; i++)
            {
                var target = _ncpShopItems[i];
                Destroy(target.gameObject);
            }
            _ncpShopItems.Clear();
        
            for (int i = 0; i < _playerShopItems.Count; i++)
            {
                var target = _playerShopItems[i];
                Destroy(target.gameObject);
            }
            _playerShopItems.Clear();
        
            for (int i = 0; i < _inventory.Items.Count; i++)
            {
                var item = _inventory.Items[i];
                if (item != null && item is TradableItem)
                {
                    ShopItemSlot slot = Instantiate(_shopItemPrefab, Vector3.zero, Quaternion.identity.normalized, _npcItemsParent);
                    slot.LoadItem((TradableItem)item, false);
                    slot.Button.onClick.AddListener(() => BuyItemFromNpc(slot));
                    _ncpShopItems.Add(slot);
                }
            }

            Inventory playerInventory = PlayerController.Instance.EquipmentInventory;
            for (int i = 0; i < playerInventory.Items.Count; i++)
            {
                var item = playerInventory.Items[i];
                if (item != null && item is TradableItem)
                {
                    ShopItemSlot slot = Instantiate(_shopItemPrefab, Vector3.zero, Quaternion.identity.normalized, _playerItemsParent);
                    slot.LoadItem((TradableItem)item, true);
                    slot.Button.onClick.AddListener(() => SellItemToNpc(slot));
                    _playerShopItems.Add(slot);
                }
            }
        
            _canvasGroupFade.FadeIn();
        }

        public void EndInteract()
        {
            _canvasGroupFade.FadeOut();
            PlayerController.Instance.Interaction.EndInteraction();
        }

        public bool CanInteract()
        {
            return true;
        }

        public void EnterInRange()
        {
        }

        public void ExitRange()
        {
        }

        public Transform GetTransform()
        {
            return transform;
        }
        
        public void BuyItemFromNpc(ShopItemSlot itemSlot)
        {
            if (CurrencyController.Instance.GetCurrencyItem().Amount > itemSlot.Item.BuyAmount)
            {
                _inventory.RemoveItem(itemSlot.Item);
                PlayerController.Instance.CurrencyController.RemoveCurrency(itemSlot.Item.BuyAmount);
                PlayerController.Instance.EquipmentInventory.AddItem(itemSlot.Item);
        
                itemSlot.Button.onClick.RemoveAllListeners();
                itemSlot.Button.onClick.AddListener(() => SellItemToNpc(itemSlot));

                itemSlot.transform.SetParent(_playerItemsParent);
                itemSlot.LoadItem(itemSlot.Item,true);
                _playerShopItems.Add(itemSlot);
            }
        }

        public void SellItemToNpc(ShopItemSlot itemSlot)
        {
            _inventory.AddItem(itemSlot.Item);
            PlayerController.Instance.EquipmentInventory.RemoveItem(itemSlot.Item);
            PlayerController.Instance.CurrencyController.AddCurrency(itemSlot.Item.SellAmount);
        
            itemSlot.Button.onClick.RemoveAllListeners();
            itemSlot.Button.onClick.AddListener(() => BuyItemFromNpc(itemSlot));
            itemSlot.transform.SetParent(_npcItemsParent);
            itemSlot.LoadItem(itemSlot.Item,false);
            _ncpShopItems.Add(itemSlot);
        }
    }
}