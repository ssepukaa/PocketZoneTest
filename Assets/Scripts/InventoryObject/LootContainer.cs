using Assets.Scripts.InventoryObject.Abstract;
using Assets.Scripts.InventoryObject.Items;
using UnityEngine;

namespace Assets.Scripts.InventoryObject {
    public class LootContainer : MonoBehaviour, ILootContainer {

        private SpriteRenderer _renderer;
        //private IInventorySlot _slot;
        private IInventoryItem _item;
        public IInventoryItemInfo ItemInfo;

        private void Start() {
            _renderer = GetComponent<SpriteRenderer>();
          //  _slot = new InventorySlot();
        }

        public bool Construct(IInventoryItemInfo info) {
            ItemInfo = info;
            _item = new InventoryItem(info);
            _renderer.sprite = info.SpriteIcon;

            return true;
        }

        public IInventoryItem CollectLoot() {
            var collectedItem = _item;
            _item = null;
            Destroy(gameObject);
            return collectedItem;
        }
    }
}