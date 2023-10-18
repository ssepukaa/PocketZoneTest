using Assets.Scripts.InventoryObject.Abstract;
using Assets.Scripts.InventoryObject.Data;
using UnityEngine;

namespace Assets.Scripts.InventoryObject {
    public class LootContainerHandle : MonoBehaviour, ILootContainer {
        // Ручной контейнер лута для тестирования

        public InventoryItemInfo ItemInfo;
        public int Amount;
        SpriteRenderer _renderer;
        IInventorySlot _slot;
        IInventoryItem _item;

        void Start() {
            _renderer = GetComponent<SpriteRenderer>();
            _item = new InventoryItem(ItemInfo);
            Debug.Log($"LootHandler Item Amount = {_item.Amount}");
            _renderer.sprite = ItemInfo.SpriteIcon;
            _item.Amount = Amount;
           
            
        }

        
        public IInventoryItem TryLootCollect() {
            var collectedItem = _item;
            
            return collectedItem;
        }

        public void LootCollected() {
            _item = null;
            Destroy(gameObject);
        }
    }
}