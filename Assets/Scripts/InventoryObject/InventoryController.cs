using System;
using Assets.Scripts.InventoryObject.Abstract;
using Assets.Scripts.InventoryObject.Items;

namespace Assets.Scripts.InventoryObject {
    public class InventoryController {

        private Inventory _inventory;

        public InventoryController(Inventory inventory) {
            _inventory = inventory;
        }

        // Method to add an item to the _inventory
        public bool AddItem(object sender, IInventoryItem item) {
            return _inventory.TryToAdd(sender, item);
        }

        // Method to remove an item from the _inventory
        public void RemoveItem(object sender, Type itemType, int amount = 1) {
            _inventory.Remove(sender, itemType, amount);
        }

        // Method to pick up loot and add it to the _inventory
        public void PickupLoot(object sender,IInventoryItem lootItem) {
            AddItem(sender, lootItem);
        }

        // Method to equip an item
        public void EquipItem(IInventoryItem item) {
            if (_inventory.HasItem(item.GetType(), out var existingItem)) {
                existingItem.GetState().IsItemEquipped = true;
            }
        }

        // Method to unequip an item
        public void UnequipItem(IInventoryItem item) {
            if (_inventory.HasItem(item.GetType(), out var existingItem)) {
                existingItem.GetState().IsItemEquipped = false;
            }
        }

        // Additional methods can be added as needed...


    }
}