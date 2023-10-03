using Assets.Scripts.InventoryObject.Data;
using UnityEngine;

namespace Assets.Scripts.InventoryObject.Abstract {
    public interface IInventoryItemInfo {
        InventoryItemType ItemType { get; }
        string Id { get; }
        string Title { get; }
        string Description { get; }
        public int MaxAmountSlot { get; }
        Sprite SpriteIcon { get; }

    }
}
