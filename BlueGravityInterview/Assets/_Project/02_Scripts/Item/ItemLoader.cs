using Interaction;
using Player;
using UnityEngine;

namespace Items
{
    public class ItemLoader : MonoBehaviour , IInteractable
    {
        public Item LoadedItem => _loadedItem;
        
        [SerializeField]
        private Item _loadedItem;

        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [SerializeField]
        private InteractionTip _interactionTip;

        public void SetupItem(Item item)
        {
            _loadedItem = Instantiate(item);
            _spriteRenderer.sprite = _loadedItem.Sprite;
        }

        public void AddToStack()
        {
            if(_loadedItem.IsStackable)
                _loadedItem.Amount++;  
        }

        public void BeginInteract()
        {
            PlayerController.Instance.AddItem(_loadedItem);
            Destroy(gameObject);
        }

        public void EndInteract()
        {
        }

        public bool CanInteract()
        {
            return true;
        }

        public void EnterInRange()
        {
            _interactionTip.SetEnabled(true);
        }

        public void ExitRange()
        { 
            if(this != null)
                _interactionTip.SetEnabled(false);
        }

        public Transform GetTransform()
        {
            return transform;
        }
    }
}