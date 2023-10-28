using UnityEngine;

namespace Items
{
    public enum ItemSlotType
    {
        None,
        Head,
        Body,
        Leg
    }

    [CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObject/Item/New Equipment", order = 0)]
    public class EquipmentItem : TradableItem
    {
        public ItemSlotType ItemSlotType;
        public bool IsDefaultItem;

        [Header("Head")]
        public Sprite hoodSprite;

        public Sprite faceSprite;

        [Header("Body")]
        public Sprite shoulderLeft;

        public Sprite shoulderRight;
        public Sprite elbowLeft;

        public Sprite elbowRight;
        public Sprite torso;

        [Header("Leg")]
        public Sprite legLeft;

        public Sprite legRight;
        public Sprite bootLeft;
        public Sprite bootRight;
    }
}