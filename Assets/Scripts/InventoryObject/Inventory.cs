using System;
using System.Linq;
using Assets.Scripts.InventoryObject.Abstract;
using Assets.Scripts.UI.InventoryUI;
using UnityEngine;

namespace Assets.Scripts.InventoryObject {
    public class Inventory {
        public event Action<object, IInventoryItem, int> OnInventoryItemAddedEvent;
        public event Action<object, Type, int> OnInventoryItemRemovedEvent;
        public event Action<object> OnInventoryStateChangedEvent;

        public int Capacity { get; private set; }
        public bool IsFull => Slots.All(slot => slot.GetIsFull());
        public InventorySlot[] Slots { get; private set; }

        private InventorySlot FirstSelectedSlot;
        private InventorySlot SecondSelectedSlot;
        public UIInventory UIInventory;
        public Inventory(int capacity) {
            Capacity = capacity;
            InitializeSlots(capacity);
        }

        private void InitializeSlots(int capacity) {
            Slots = new InventorySlot[capacity];
            for (int i = 0; i < capacity; i++) {
                Slots[i] = new InventorySlot();
            }
        }

        public void ButtonSlotSelected(int slotIndex) {
            SelectSlot(GetSlotByIndex(slotIndex));
        }

        // Публичный метод для доступа к первому выделенному слоту

        public InventorySlot GetFirstSelectedSlot() {
            return FirstSelectedSlot;
        }

        public void SelectSlot(InventorySlot slot) {
            // Если оба слота пусты, то ничего не делаем.
            if (FirstSelectedSlot == null && (slot == null || slot.GetIsEmpty())) return;

            // Если первый слот не выбран, то устанавливаем его.
            if (FirstSelectedSlot == null) {
                SetFirstSelectedSlot(slot);
                return;
            }

            // Если первый слот выбран и второй слот пуст, то переносим предмет из первого слота во второй.
            if (slot != null && slot.GetIsEmpty()) {
                TransitFromSlotToSlot(this, FirstSelectedSlot, slot);
                DeselectAllSlots();
                return;
            }

            // Если первый слот выбран и второй слот также выбран (но не пуст), то передаем оба слота в метод TransitFromSlotToSlot.
            if (slot != null && !slot.GetIsEmpty()) {
                SetSecondSelectedSlot(slot);
                if (BothSlotsSelected()) {
                    TransitFromSlotToSlot(this, FirstSelectedSlot, SecondSelectedSlot);
                    DeselectAllSlots();
                }
                return;
            }
        }




        private void SetFirstSelectedSlot(InventorySlot slot) {
            FirstSelectedSlot = slot;
            FirstSelectedSlot.Select();
            UIInventory._uiSlots[GetIndexOfSelectedSlot(FirstSelectedSlot)].Select();
        }

        private void SetSecondSelectedSlot(InventorySlot slot) {
            SecondSelectedSlot = slot;
            SecondSelectedSlot.Select();
            //UIInventory._uiSlots[GetIndexOfSelectedSlot(SecondSelectedSlot)].Select();
        }

        private bool BothSlotsSelected() {
            return FirstSelectedSlot != null && SecondSelectedSlot != null;
        }

        public void DeselectAllSlots() {
            DeselectSlot(ref FirstSelectedSlot);
            DeselectSlot(ref SecondSelectedSlot);

        }

        private void DeselectSlot(ref InventorySlot slot) {
            if (slot != null) {
                slot.Deselect();
                UIInventory._uiSlots[GetIndexOfSelectedSlot(slot)].Deselect();
                slot = null;
            }
        }

        public int GetIndexOfSelectedSlot(InventorySlot selectedSlot) {
            return Array.IndexOf(Slots, selectedSlot);
        }

        public IInventoryItem GetItem(Type itemType) {
            return Slots.FirstOrDefault(slot => slot.GetItem().GetType() == itemType)?.GetItem();

        }

        public IInventoryItem[] GetAllItems() {
            return Slots.Where(slot => !slot.GetIsEmpty()).Select(slot => slot.GetItem()).ToArray();

        }

        public IInventoryItem[] GetAllTypes(Type itemType) {
            return Slots.Where(slot => !slot.GetIsEmpty() && slot.GetItem().GetType() == itemType).Select(slot => slot.GetItem()).ToArray();

        }

        public IInventoryItem[] GetEquippedItems() {
            return Slots.Where(slot => !slot.GetIsEmpty() && slot.GetItem().GetState().GetIsItemEquipped()).Select(slot => slot.GetItem()).ToArray();
        }

        public int GetItemAmount(Type itemType) {
            return Slots.Where(slot => !slot.GetIsEmpty() && slot.GetItem().GetType() == itemType).Sum(slot => slot.GetAmount());
        }


        public bool TryToAdd(object sender, IInventoryItem item) {
            // Поиск слота с тем же типом предмета, который не пуст и не полон
            var slotWithSameItemButNotEmpty = Slots
                .FirstOrDefault(slot => !slot.GetIsEmpty() && !slot.GetIsFull() && slot.GetItem().GetType() == item.GetType());

            if (slotWithSameItemButNotEmpty != null)
                return TryAddToSlot(sender, slotWithSameItemButNotEmpty, item);

            // Поиск пустого слота
            var emptySlot = Slots.FirstOrDefault(slot => slot.GetIsEmpty());
            if (emptySlot != null)
                return TryAddToSlot(sender, emptySlot, item);

            Debug.Log("Нет места для предметов в инвентаре!!!");
            return false;
        }

        public bool TryAddToSlot(object sender, InventorySlot slot, IInventoryItem item) {
            var fits = slot.GetAmount() + item.GetState().GetAmount() <= item.GetInfo().maxAmountSlot;
            var amountToAdd = fits
                ? item.GetState().GetAmount()
                : item.GetInfo().maxAmountSlot - slot.GetAmount();
            var amountLeft = item.GetState().GetAmount() - amountToAdd;
            var clonedItem = item.Clone();
            clonedItem.GetState().SetAmount(amountToAdd);

            if (slot.GetIsEmpty())
                slot.SetItem(clonedItem);
            else
                slot.GetItem().GetState().AddAmount(amountToAdd);

            Debug.Log($"Item added to _inventory. InventoryItemType: {item.GetType()}, amount: {amountToAdd}");
            OnInventoryItemAddedEvent?.Invoke(sender, item, amountToAdd);
            OnInventoryStateChangedEvent?.Invoke(sender);

            if (amountLeft <= 0)
                return true;

            item.GetState().SetAmount(amountLeft);
            return TryToAdd(sender, item);
        }


        public void TransitFromSlotToSlot(object sender, InventorySlot fromSlot, InventorySlot toSlot) {
            if (fromSlot.GetIsEmpty()) return;
            if (toSlot.GetIsFull()) return;
            if (!toSlot.GetIsEmpty() && fromSlot.GetItem().GetType() !=toSlot.GetItem().GetType()) return;
            if (fromSlot == toSlot) return;

            
            var slotCapacity = fromSlot.GetItem().GetInfo().maxAmountSlot;
            //Поместятся ли все предметы
            var fits = fromSlot.GetAmount() + toSlot.GetAmount() <= slotCapacity;
            //Если да, то добавляем amount, если не помещаются, то определим остаток
            var amountToAdd = fits ? fromSlot.GetAmount() : slotCapacity - toSlot.GetAmount();
            //Запишем остаток в переменную
            var amountLeft = fromSlot.GetAmount() - amountToAdd;
            if (toSlot.GetIsEmpty()) {
                toSlot.SetItem(fromSlot.GetItem());
                fromSlot.Clear();
                OnInventoryStateChangedEvent?.Invoke(sender);
            }
            
            toSlot.GetItem().GetState().AddAmount(amountToAdd);
            if (fits) {
                fromSlot.Clear();
            } else {
                fromSlot.GetItem().GetState().SetAmount(amountLeft);
            }
            OnInventoryStateChangedEvent?.Invoke(sender);            
        }


        public void Remove(object sender, Type itemType, int amount = 1) {
            var slotsWithItem = GetAllSlots(itemType);
            if (slotsWithItem.Length == 0) return;

            var amountToRemove = amount;

            foreach (var slot in slotsWithItem) {
                if (amountToRemove <= 0) break;

                if (slot.GetAmount() >= amountToRemove) {
                    slot.GetItem().GetState().RemoveAmount(amountToRemove);
                    if (slot.GetAmount() <= 0) slot.Clear();

                    Debug.Log($"Item removed from _inventory. InventoryItemType: {itemType}, amount: {amountToRemove}");
                    OnInventoryItemRemovedEvent?.Invoke(sender, itemType, amountToRemove);
                    OnInventoryStateChangedEvent?.Invoke(sender);
                    break;
                } else {
                    amountToRemove -= slot.GetAmount();
                    Debug.Log($"Item removed from _inventory. InventoryItemType: {itemType}, amount: {slot.GetAmount()}");
                    OnInventoryItemRemovedEvent?.Invoke(sender, itemType, slot.GetAmount());
                    slot.Clear();
                    OnInventoryStateChangedEvent?.Invoke(sender);
                }
            }
        }


        public bool HasItem(Type itemType, out IInventoryItem item) {
            item = GetItem(itemType);
            return item != null;
        }


        public InventorySlot[] GetAllSlots(Type itemType) {
            return Slots.Where(slot => !slot.GetIsEmpty() && slot.GetItem().GetType() == itemType).ToArray();
        }

        public InventorySlot[] GetAllSlots() {
            return Slots;
        }

        public InventorySlot GetSlotByIndex(int index) {
            if (index < 0 || index >= Slots.Length) {
                throw new ArgumentOutOfRangeException(nameof(index), "Индекс вне диапазона слотов инвентаря.");
            }
            return Slots[index];
        }
    }
}





// using System;
// using System.Collections.Generic;
// using System.Linq;
// using Assets.Scripts.Inventory.Abstract;
// using UnityEngine;
//
//
// namespace Assets.Scripts.Inventory {
//     public class Inventory : IInventory {
//         public event Action<object, IInventoryItem, int> OnInventoryItemAddedEvent;
//         public event Action<object, Type, int> OnInventoryItemRemovedEvent;
//         public event Action<object> OnInventoryStateChangedEvent;
//         public int Capacity { get; set; }
//         public bool GetIsFull => Slots.All(slot => slot.GetIsFull);
//         private List<IInventorySlot> Slots;
//
//         public Inventory(int Capacity) {
//             this.Capacity = Capacity;
//             Slots = new List<IInventorySlot>();
//             for (int i = 0; i < Capacity; i++) {
//                 Slots.Add(new InventorySlot());
//             }
//         }
//
//         public IInventoryItem GetItem(Type inventoryItemType) {
//             return Slots.Find(slot => slot.inventoryItemType == inventoryItemType).item;
//         }
//
//         public IInventoryItem[] GetAllItems() {
//             var allItems = new List<IInventoryItem>();
//             foreach (var slot in Slots) {
//                 if (!slot.isEmpty)
//                     allItems.Add(slot.item);
//             }
//             return allItems.ToArray();
//         }
//
//         public IInventoryItem[] GetAllTypes(Type inventoryItemType) {
//             var allItemsOfType = new List<IInventoryItem>();
//             var slotsOfType = Slots
//                 .FindAll(slot => !slot.isEmpty && slot.inventoryItemType == inventoryItemType);
//             foreach (var slot in slotsOfType) {
//                 allItemsOfType.Add(slot.item);
//             }
//             return allItemsOfType.ToArray();
//         }
//
//         public IInventoryItem[] GetEquippedItems() {
//             var requiredSlots = Slots
//                 .FindAll(slot => !slot.isEmpty && slot.item.State.isEquipped);
//             var equippedItems = new List<IInventoryItem>();
//             foreach (var slot in requiredSlots) {
//                 equippedItems.Add(slot.item);
//             }
//             return equippedItems.ToArray();
//         }
//
//         public int GetItemAmount(Type inventoryItemType) {
//             var amount = 0;
//             var allItemSlots = Slots
//                 .FindAll(slot => !slot.isEmpty && slot.inventoryItemType == inventoryItemType);
//             foreach (var slot in allItemSlots) {
//                 amount += slot.amount;
//             }
//             return amount;
//         }
//
//
//         public bool TryToAdd(object sender, IInventoryItem item) {
//             var slotWithSameItemButNotEmpty = Slots
//                 .Find(slot => !slot.isEmpty && !slot.GetIsFull && slot.inventoryItemType == item.type);
//             if (slotWithSameItemButNotEmpty != null)
//                 return TryAddToSlot(sender, slotWithSameItemButNotEmpty, item);
//
//
//             var emptySlot = Slots.Find(slot => slot.isEmpty);
//             if (emptySlot != null)
//                 return TryAddToSlot(sender, emptySlot, item);
//             Debug.Log("Нет места для предметов в инвентаре!!!");
//             return false;
//         }
//
//         public bool TryAddToSlot(object sender, IInventorySlot slot, IInventoryItem item) {
//             var fits = slot.amount + item.State.amount <= item.Info.maxAmountSlot;
//             var amountToAdd = fits
//                 ? item.State.amount
//                 : item.Info.maxAmountSlot - slot.amount;
//             var amountLeft = item.State.amount - amountToAdd;
//             var clonedItem = item.Clone();
//             clonedItem.State.amount = amountToAdd;
//             if (slot.isEmpty)
//                 slot.SetItem(clonedItem);
//             else
//                 slot.item.State.amount += amountToAdd;
//             Debug.Log($"Item added to _inventory. InventoryItemType: {item.type}, amount: {amountToAdd}");
//             OnInventoryItemAddedEvent?.Invoke(sender, item, amountToAdd);
//             OnInventoryStateChangedEvent?.Invoke(sender);
//
//             if (amountLeft <= 0)
//                 return true;
//             item.State.amount = amountLeft;
//             return TryToAdd(sender, item);
//
//         }
//
//         public void TransitFromSlotToSlot(object sender, IInventorySlot fromSlot, IInventorySlot toSlot) {
//             if (fromSlot.isEmpty) return;
//             if (toSlot.GetIsFull) return;
//             if (!toSlot.isEmpty && fromSlot.inventoryItemType !=toSlot.inventoryItemType) return;
//             if (fromSlot == toSlot) return;
//
//             var slotCapacity = fromSlot.Capacity;
//             //Поместятся ли все предметы
//             var fits = fromSlot.amount + toSlot.amount <= slotCapacity;
//             //Если да, то добавляем amount, если не помещаются, то определим остаток
//             var amountToAdd = fits ? fromSlot.amount : slotCapacity - toSlot.amount;
//             //Запишем остаток в переменную
//             var amountLeft = fromSlot.amount - amountToAdd;
//             if (toSlot.isEmpty) {
//                 toSlot.SetItem(fromSlot.item);
//                 fromSlot.Clear();
//                 OnInventoryStateChangedEvent?.Invoke(sender);
//             }
//
//             toSlot.item.State.amount += amountToAdd;
//             if (fits) {
//                 fromSlot.Clear();
//             } else {
//                 fromSlot.item.State.amount = amountLeft;
//             }
//             OnInventoryStateChangedEvent?.Invoke(sender);
//         }
//
//         public void Remove(object sender, Type inventoryItemType, int amount = 1) {
//             var slotsWithItem = GetAllSlots(inventoryItemType);
//             if (slotsWithItem.Length == 0)
//                 return;
//             var amountToRemove = amount;
//             var count = slotsWithItem.Length;
//             for (int i = count - 1; i >= 0; i--) {
//                 var slot = slotsWithItem[i];
//                 if (slot.amount >= amountToRemove) {
//                     slot.item.State.amount -= amountToRemove;
//                     if (slot.amount <= 0)
//                         slot.Clear();
//
//                     Debug.Log($"Item removed from _inventory. InventoryItemType: {inventoryItemType}," +
//                               $" amount: {amountToRemove}");
//
//                     OnInventoryItemRemovedEvent?.Invoke(sender, inventoryItemType, amountToRemove);
//                     OnInventoryStateChangedEvent?.Invoke(sender);
//                     break;
//                 }
//
//                 var amountToRemoved = slot.amount;
//                 amountToRemove-= slot.amount;
//                 slot.Clear();
//                 Debug.Log($"Item removed from _inventory. InventoryItemType: {inventoryItemType}," +
//                           $" amount: {amountToRemoved}");
//                 OnInventoryItemRemovedEvent?.Invoke(sender, inventoryItemType, amountToRemoved);
//                 OnInventoryStateChangedEvent?.Invoke(sender);
//             }
//         }
//
//         public bool HasItem(Type inventoryItemType, out IInventoryItem item) {
//             item = GetItem(inventoryItemType);
//             return item != null;
//         }
//
//         public IInventorySlot[] GetAllSlots(Type inventoryItemType) {
//             return Slots.
//                 FindAll(slot => !slot.isEmpty && slot.inventoryItemType == inventoryItemType).ToArray();
//
//         }
//
//         public IInventorySlot[] GetAllSlots() {
//             return Slots.ToArray();
//         }
//         public IInventorySlot GetSlotByIndex(int index) {
//             if (index < 0 || index >= Slots.Count) {
//                 throw new ArgumentOutOfRangeException(nameof(index), "Индекс вне диапазона слотов инвентаря.");
//             }
//             return Slots[index];
//         }
//
//     }
// }
//
