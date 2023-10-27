using System.Collections.Generic;
using Items;
using UnityEngine;

namespace Interaction
{
    public class Chest : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private Animator _anim;

        [SerializeField]
        private ItemLoader _itemLoaderPrefab;

        [SerializeField]
        private Item[] _itemsPool = new Item[] { };

        [SerializeField]
        private int _itemsPerLoot = 2;

        private bool _canLoot = true;
        private static readonly int Open = Animator.StringToHash("Open");
        private List<Item> _loot = new List<Item>();

        public void BeginInteract()
        {
            _canLoot = false;
            _anim.SetTrigger(Open);
            RandomizeLoot();
            SpawnLoot();
        }

        public void EndInteract()
        {
        }

        public bool CanInteract()
        {
            return _canLoot;
        }

        public void EnterInRange()
        {
        }

        public void ExitRange()
        {
        }

        private void RandomizeLoot()
        {
            int itemsChoosed = 0;
            int attempt = 0;
            int maxAttempts = 20;
            while (itemsChoosed < _itemsPerLoot)
            {
                if(attempt >= maxAttempts)
                    break;
                
                var rndIndex = UnityEngine.Random.Range(0, _itemsPool.Length);
                var item = _itemsPool[rndIndex];
                
                if ((_loot.Contains(item) && item.IsStackable))
                {
                    item = _loot.Find(x => x.Sprite == item.Sprite);
                    item.Amount++;
                    itemsChoosed++;
                }

                if (!_loot.Contains(item))
                {
                    var itemLoader = Instantiate(_itemLoaderPrefab);
                    itemLoader.SetupItem(item);
                    _loot.Add(item);
                    itemsChoosed++;
                }

                attempt++;
            }
        }
        
        private void SpawnLoot()
        {
            for (int i = 0; i < _loot.Count; i++)
            {
                ItemLoader itemLoader = Instantiate(_itemLoaderPrefab, transform.position, Quaternion.identity);
                itemLoader.SetupItem(_loot[i]);
            }
        }

        public Transform GetTransform()
        {
            return transform;
        }
    }
}