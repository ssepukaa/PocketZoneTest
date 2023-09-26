using System;

namespace Assets.Scripts.Inventory.Abstract {
    public interface IInventory {
        int capacity { get; set; }
        bool isFull { get; }
        IInventoryItem GetItem(Type itemType);
        IInventoryItem[] GetAllItems();
        IInventoryItem[] GetAllTypes(Type itemType);
        IInventoryItem[] GetEquippedItems();
        int GetItemAmount(Type itemType);
        bool TryToAdd(object sender, IInventoryItem item);
        void Remove(object sender, Type itemType, int amount = 1);
        bool HasItem(Type itemType, out IInventoryItem item);
    }
}
