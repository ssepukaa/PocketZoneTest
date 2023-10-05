using Assets.Scripts.InventoryObject.Data;
using UnityEngine;

namespace Assets.Scripts.InventoryObject.Abstract {
    public interface IInventoryItemInfo {
        InventoryItemType ItemType { get; }
        public ItemFunctionalityType FunctionalityType { get; }
        public ItemIsEquippableType ItemEquippableType { get; }
        public ItemAmmoType AmmoType { get; }

        string Id { get; }
        string Title { get; }
        string Description { get; }
        public int MaxAmountSlot { get; }
        Sprite SpriteIcon { get; }
        public bool IsEquip { get; }
        Sprite BulletSprite { get; }
    }
}
