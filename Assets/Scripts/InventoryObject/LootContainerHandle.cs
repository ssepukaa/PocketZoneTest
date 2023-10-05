using Assets.Scripts.InventoryObject.Abstract;
using Assets.Scripts.InventoryObject.Data;
using UnityEngine;

namespace Assets.Scripts.InventoryObject {
    public class LootContainerHandle : MonoBehaviour, ILootContainer {

        private SpriteRenderer _renderer;
        private IInventorySlot _slot;
        private IInventoryItem _item;
        public InventoryItemInfo ItemInfo;
        public int Amount;
        void Start() {
            _renderer = GetComponent<SpriteRenderer>();
            _item = new InventoryItem(ItemInfo);
            Debug.Log($"LootHandler Item Amount = {_item.Amount}");
            _renderer.sprite = ItemInfo.SpriteIcon;
            _item.Amount = Amount;
           
            
        }

        
        public IInventoryItem CollectLoot() {
            var collectedItem = _item;
            _item = null;
            Destroy(gameObject);
            return collectedItem;
        }
    }
}