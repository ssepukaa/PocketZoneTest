using System;
using Assets.Scripts.InventoryObject.Data;

namespace Assets.Scripts.InventoryObject.Abstract {
    // Инвентарь
    public interface IInventory {
        event Action<object, IInventoryItem, int> OnInventoryItemAddedEvent;
        event Action<object, InventoryItemType, int> OnInventoryItemRemovedEvent;
        event Action<object> OnInventoryStateChangedEvent;
        event Action<object, IInventoryItemInfo, int> OnOneItemInSelectedSlotDroppedEvent;
        event Action<object, IInventoryItemInfo, int> OnRemoveOneAmountItemInSelectedSlotEquippedEvent;
        event Action<object, IInventoryItem, int, int> OnOneItemAmmoRemovedEvent;
        event Action OnAmmoChangedEvent;

        //bool IsFull { get; }
        IInventorySlot ClipSlot { get; }
        IInventorySlot[] SlotsArray { get; set; }
        IInventorySlot WeaponSlot { get; }
        IInventorySlot GetSlotByIndex(int index);
        IInventorySlot GetSelectedSlot();
        IInventoryItem GetItem(InventoryItemType itemType);
        IInventorySlot GetAmmoSlotByType(ItemAmmoType ammoType);
        IInventoryItem[] GetAllItems();
        IInventoryItem[] GetAllTypes(InventoryItemType itemType);
        IInventoryItem[] GetEquippedItems();
        int Capacity { get; set; }

        int GetItemAmount(InventoryItemType itemType);
        bool TryToAdd(object sender, IInventoryItem item);
        bool TryAddToSlot(object sender, IInventorySlot slot, IInventoryItem item);
        void ButtonSlotSelected(IInventorySlot slot);
        void Remove(object sender, InventoryItemType itemType, int amount = 1);
        bool RemoveOneAmountItemInSelectedSlotDropped(object sender);
        bool RemoveOneAmountItemInAmmoByType(object sender, ItemAmmoType ammoType);
        bool ShootingAndRemoveAmmo(object sender);
        bool EquipItem(object sender);
        bool HasItem(InventoryItemType itemType, out IInventoryItem item);
        int GetTotalAmmoByType(ItemAmmoType itemType);
        bool ReloadClipSlot();
    }
}
