using InventorySystem;
using Items;
using UnityEngine;

namespace Player
{
    public class PlayerVisual : MonoBehaviour
    {
        [SerializeField]
        private EquipmentInventory _inventory;

        [Header("Head")]
        public SpriteRenderer hood;

        public SpriteRenderer face;

        [Header("Body")]
        public SpriteRenderer shoulderLeft;

        public SpriteRenderer shoulderRight;
        public SpriteRenderer elbowLeft;

        public SpriteRenderer elbowRight;
        public SpriteRenderer torso;

        [Header("Leg")]
        public SpriteRenderer legLeft;

        public SpriteRenderer legRight;
        public SpriteRenderer bootLeft;
        public SpriteRenderer bootRight;

        private void Awake()
        {
            _inventory.OnEquipmentChange += OnEquipmentChange;
        }

        private void OnDestroy()
        {
            _inventory.OnEquipmentChange -= OnEquipmentChange;
        }

        private void OnEquipmentChange(EquipmentItem item, ItemSlotType slotType)
        {
            if(item == null) return;
            ChangeVisual(item);
        }

        public void ChangeVisual(EquipmentItem item)
        {
            switch (item.ItemSlotType)
            {
                case ItemSlotType.None:
                    Debug.LogError("Error has not defined slot type!");
                    return;
                case ItemSlotType.Head:
                {
                    hood.sprite = item.hoodSprite;
                    face.sprite = item.faceSprite;
                }
                    break;
                case ItemSlotType.Body:
                {
                    shoulderLeft.sprite = item.shoulderLeft;
                    shoulderRight.sprite = item.shoulderRight;
                    elbowLeft.sprite = item.elbowLeft;
                    elbowRight.sprite = item.elbowRight;
                    torso.sprite = item.torso;
                }
                    break;
                case ItemSlotType.Leg:
                {
                    legLeft.sprite = item.legLeft;
                    legRight.sprite = item.legRight;
                    bootLeft.sprite = item.bootLeft;
                    bootRight.sprite = item.bootRight;
                }
                    break;
            }
        }
    }
}