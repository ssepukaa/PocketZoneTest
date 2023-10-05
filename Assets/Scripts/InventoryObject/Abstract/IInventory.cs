using System;
using Assets.Scripts.InventoryObject.Data;

namespace Assets.Scripts.InventoryObject.Abstract {
    public interface IInventory {
        int Capacity { get; set; }
        bool IsFull { get; }
        public IInventorySlot WeaponSlot { get; }
        public IInventorySlot GetSlotByIndex(int index);
        public IInventorySlot GetSelectedSlot();
        IInventoryItem GetItem(InventoryItemType itemType);
        public IInventorySlot GetAmmoSlotByType(ItemAmmoType ammoType);
        IInventoryItem[] GetAllItems();
        IInventoryItem[] GetAllTypes(InventoryItemType itemType);
        IInventoryItem[] GetEquippedItems();

        //int GetItemAmount(Type itemType);
        bool TryToAdd(object sender, IInventoryItem item);
        bool TryAddToSlot(object sender, IInventorySlot slot, IInventoryItem item);
        public void ButtonSlotSelected(IInventorySlot slot);
        void Remove(object sender, InventoryItemType itemType, int amount = 1);
        public bool RemoveOneAmountItemInSelectedSlotDropped(object sender);
        public bool RemoveOneAmountItemInAmmoByType(object sender, ItemAmmoType ammoType);
        public bool EquipItem(object sender);
        bool HasItem(InventoryItemType itemType, out IInventoryItem item);
        public event Action<object, IInventoryItem, int> OnInventoryItemAddedEvent;
        public event Action<object, InventoryItemType, int> OnInventoryItemRemovedEvent;
        public event Action<object> OnInventoryStateChangedEvent;
        public event Action<object, IInventoryItemInfo, int> OnOneItemInSelectedSlotDroppedEvent;
        public event Action<object, IInventoryItemInfo, int> OnRemoveOneAmountItemInSelectedSlotEquippedEvent;
        public event Action<object, IInventoryItem, int> OnOneItemAmmoRemovedEvent;




    }
}
