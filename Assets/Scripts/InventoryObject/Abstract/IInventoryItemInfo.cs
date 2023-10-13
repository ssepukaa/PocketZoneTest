using Assets.Scripts.InventoryObject.Data;
using UnityEngine;

namespace Assets.Scripts.InventoryObject.Abstract {
    public interface IInventoryItemInfo {
        InventoryItemType ItemType { get; }
        ItemFunctionalityType FunctionalityType { get; }
        ItemIsEquippableType ItemEquippableType { get; }

        WeaponItemInfo WeaponInfo { get; }
        AmmoItemInfo AmmoInfo { get; }

        string Id { get; }
        string Title { get; }
        string Description { get; }
        int MaxAmountSlot { get; }
        bool IsEquip { get; }
        Sprite SpriteIcon { get; }
    }
}
