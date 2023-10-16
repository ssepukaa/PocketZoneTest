using Assets.Scripts.InventoryObject.Abstract;
using UnityEngine;

namespace Assets.Scripts.InventoryObject {
    public class LootContainer : MonoBehaviour, ILootContainer {
        IInventoryItem _item;
        SpriteRenderer _renderer;
        //private IInventorySlot _slot;


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

        public IInventoryItem TryLootCollect() {
            var collectedItem = _item;
            collectedItem.Amount = _item.Amount;
            
            return collectedItem;
        }

        public void LootCollected() {
            _item = null;
            Destroy(gameObject);
        }

    }
}