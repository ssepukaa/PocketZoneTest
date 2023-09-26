using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Inventory.Abstract;
using UnityEngine;
using static UnityEditor.Progress;

namespace Assets.Scripts.Inventory {
    public class InventoryWithSlots: IInventory {
        public event Action<object, IInventoryItem, int> OnInventoryItemAddedEvent;
        public event Action<object, Type, int> OnInventoryItemRemovedEvent;
        public int capacity { get; set; }
        public bool isFull => _slots.All(slot => slot.isFull);
        private List<IInventorySlot> _slots;

        public InventoryWithSlots(int capacity) {
            this.capacity = capacity;
            _slots = new List<IInventorySlot>();
            for (int i = 0; i < capacity; i++) {
                _slots.Add(new InventorySlot());
            }
        }

        public IInventoryItem GetItem(Type itemType) {
            return _slots.Find(slot=> slot.itemType == itemType).item;
        }

        public IInventoryItem[] GetAllItems() {
            var allItems = new List<IInventoryItem>();
            foreach (var slot in _slots) {
                if (!slot.isEmpty) 
                    allItems.Add(slot.item);
            }
            return allItems.ToArray();
        }

        public IInventoryItem[] GetAllTypes(Type itemType) {
            var allItemsOfType = new List<IInventoryItem>();
            var slotsOfType = _slots
                .FindAll(slot => !slot.isEmpty && slot.itemType == itemType);
            foreach (var slot in slotsOfType) {
                allItemsOfType.Add(slot.item);
            }
            return allItemsOfType.ToArray();
        }

        public IInventoryItem[] GetEquippedItems() {
            var requiredSlots = _slots
                .FindAll(slot => !slot.isEmpty && slot.item.isEquipped);
            var equippedItems = new List<IInventoryItem>();
            foreach (var slot in requiredSlots) {
                equippedItems.Add(slot.item);
            }
            return equippedItems.ToArray();
        }

        public int GetItemAmount(Type itemType) {
            var amount = 0;
            var allItemSlots = _slots
                .FindAll(slot => !slot.isEmpty && slot.itemType == itemType);
            foreach (var slot in allItemSlots) {
                amount += slot.amount;
            }
            return amount;
        }


        public bool TryToAdd(object sender, IInventoryItem item) {
            var slotWithSameItemButNotEmpty = _slots
                .Find(slot => !slot.isEmpty && !slot.isFull && slot.itemType == item.type);
            if (slotWithSameItemButNotEmpty != null) 
                return TryAddToSlot(sender, slotWithSameItemButNotEmpty, item);
            

            var emptySlot = _slots.Find(slot => slot.isEmpty);
            if (emptySlot != null)
                return TryAddToSlot(sender, emptySlot, item);
            Debug.Log("Нет места для предметов в инвентаре!!!");
            return false;
        }

        private bool TryAddToSlot(object sender, IInventorySlot slot, IInventoryItem item) {
            var fits = slot.amount + item.amount <= item.maxItemsInInventorySlot;
            var amountToAdd = fits
                ? item.amount 
                : item.maxItemsInInventorySlot - slot.amount;
            var amountLeft = item.amount - amountToAdd;
            var clonedItem = item.Clone();
            clonedItem.amount = amountToAdd;
            if (slot.isEmpty)
                slot.SetItem(clonedItem);
            else
                slot.item.amount += amountToAdd;
            Debug.Log($"Item added to inventory. ItemType: {item.type}, amount: {amountToAdd}");
            OnInventoryItemAddedEvent?.Invoke(sender, item, amountToAdd);

            if (amountLeft <= 0)
                return true;
            item.amount = amountLeft;
            return TryToAdd(sender, item);

        }

        public void Remove(object sender, Type itemType, int amount = 1) {
            var slotsWithItem = GetAllSlots(itemType);
            if(slotsWithItem.Length == 0)
                return;
            var amountToRemove = amount;
            var count = slotsWithItem.Length;
            for (int i = count - 1; i >= 0; i--) {
                var slot = slotsWithItem[i];
                if (slot.amount >= amountToRemove) {
                    slot.item.amount -= amountToRemove;
                    if(slot.amount <= 0)
                        slot.Clear();

                    Debug.Log($"Item removed from inventory. ItemType: {itemType}," +
                              $" amount: {amountToRemove}");

                    OnInventoryItemRemovedEvent?.Invoke(sender, itemType, amountToRemove);
                    break;
                }

                var amountToRemoved = slot.amount;
                amountToRemove-= slot.amount;
                slot.Clear();
                Debug.Log($"Item removed from inventory. ItemType: {itemType}," +
                          $" amount: {amountToRemoved}");
                OnInventoryItemRemovedEvent?.Invoke(sender, itemType, amountToRemoved);
            }
        }

        public bool HasItem(Type itemType, out IInventoryItem item) {
            item = GetItem(itemType);
            return item != null;
        }

        public IInventorySlot[] GetAllSlots(Type itemType) {
            return _slots.
                FindAll(slot => !slot.isEmpty && slot.itemType == itemType).ToArray();

        }

        public IInventorySlot[] getAllSlots() {
            return _slots.ToArray();
        }
    }
}
