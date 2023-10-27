using UnityEngine;

namespace Items
{
    public enum ItemType
    {
        Common,
        Currency
    }
    
    [CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObject/Item/New Item", order = 0)]
    public class Item : ScriptableObject
    {
        public string ItemName;
        public Sprite Sprite;
        public ItemType Type;
        public bool IsStackable = false;
        public int Amount=1;
    }
}