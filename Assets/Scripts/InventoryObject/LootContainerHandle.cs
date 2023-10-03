using Assets.Scripts.InventoryObject.Abstract;
using Assets.Scripts.InventoryObject.Data;
using Assets.Scripts.InventoryObject.Items;
using UnityEngine;
using static UnityEngine.AdaptivePerformance.Provider.AdaptivePerformanceSubsystemDescriptor;

namespace Assets.Scripts.InventoryObject {
    public class LootContainerHandle : MonoBehaviour, ILootContainer {

        private SpriteRenderer _renderer;
        private IInventorySlot _slot;
        private IInventoryItem _item;
        public InventoryItemInfo ItemInfo;
        void Start() {
            _renderer = GetComponent<SpriteRenderer>();
            _item = new InventoryItem(ItemInfo);
            _renderer.sprite = ItemInfo.SpriteIcon;
            
        }

        
        public IInventoryItem CollectLoot() {
            var collectedItem = _item;
            _item = null;
            Destroy(gameObject);
            return collectedItem;
        }
    }
}