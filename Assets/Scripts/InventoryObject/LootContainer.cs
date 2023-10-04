using Assets.Scripts.InventoryObject.Abstract;
using Assets.Scripts.InventoryObject.Items;
using UnityEngine;

namespace Assets.Scripts.InventoryObject {
    public class LootContainer : MonoBehaviour, ILootContainer {

        private SpriteRenderer _renderer;
        //private IInventorySlot _slot;
        private IInventoryItem _item;
       

        private void Awake() {
            _renderer = GetComponent<SpriteRenderer>();
          //  _slot = new InventorySlot();
        }

        public bool Construct(IInventoryItemInfo info, int amount=1) {
           Debug.Log($":Info == null : {info == null}");
            _item = new InventoryItem(info);
            _renderer.sprite = info.SpriteIcon;
            _item.Amount = amount;
            return true;
        }

        public IInventoryItem CollectLoot() {
            var collectedItem = _item;
            collectedItem.Amount = _item.Amount;
            _item = null;
            Destroy(gameObject);
            return collectedItem;
        }
    }
}