using UnityEngine;

namespace Assets.Scripts.InventoryObject.Abstract {
    public interface IInventoryItemInfo {
        string id { get; }
        string title { get; }
        string description { get; }
        public int maxItemsInInventorySlot { get; }
        Sprite spriteIcon { get; }

    }
}
