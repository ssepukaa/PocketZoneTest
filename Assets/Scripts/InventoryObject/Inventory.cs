using System;
using System.Linq;
using Assets.Scripts.InventoryObject.Abstract;
using Assets.Scripts.InventoryObject.Data;
using Assets.Scripts.InventoryObject.Items;
using UnityEngine;

namespace Assets.Scripts.InventoryObject {
    public class Inventory : IInventory {
        public event Action<object, IInventoryItem, int> OnInventoryItemAddedEvent;
        public event Action<object, InventoryItemType, int> OnInventoryItemRemovedEvent;
        public event Action<object> OnInventoryStateChangedEvent;
        

        public int Capacity { get; set; }
        public bool IsFull => Slots.All(slot => slot.IsFull);
        public IInventorySlot[] Slots;

        private IInventorySlot _selectedSlot;

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

        public void ButtonSlotSelected(IInventorySlot slot) {
            SelectSlot(slot);
        }


        public void SelectSlot(IInventorySlot slot) {

            // Если первый слот выбран и второй слот пуст, то переносим предмет из первого слота во второй.
            if (_selectedSlot != null) {
                Debug.Log($"SelectedSlot != null ---- {_selectedSlot!=null} : Slot transit ");
                TransitFromSlotToSlot(this, _selectedSlot, slot);

                return;
            }
            // Если слот первый выбран и он нулевой , то возврат
            if (slot.IsEmpty && _selectedSlot == null) {
                Debug.Log("Select slot == null and selectedSlot == null : return");
                return;
            }

            if (slot == _selectedSlot) {
                Debug.Log("Select slot == selectedSlot : return");
                DeselectSelectedSlot();
                return;
            }

            // Если слот выбран первый, то сделать его isSelect
            if (!slot.IsEmpty && _selectedSlot == null) {
                slot.Select();
                _selectedSlot = slot;
                Debug.Log($"Select slot nit empty and selectedSlot == null ---- {_selectedSlot==null} : setup selectedSlot");
                OnInventoryStateChangedEvent?.Invoke(this);
                
                return;
            }
            Debug.Log("Никакие условия не выполнены!!");

        }

        private void DeselectSelectedSlot() {
            _selectedSlot.Deselect();
            _selectedSlot = null;
            OnInventoryStateChangedEvent?.Invoke(this);

        }

        public int GetIndexOfSelectedSlot(IInventorySlot selectedSlot) {
            return Array.IndexOf(Slots, selectedSlot);
        }

        public IInventorySlot GetSelectedSlot() {
           return Slots.FirstOrDefault(slot => slot.IsSelect);
        }

        public IInventoryItem GetItem(InventoryItemType itemType) {
            return Slots.FirstOrDefault(slot => slot.ItemType == itemType)?.Item;

        }

        public IInventoryItem[] GetAllItems() {
            return Slots.Where(slot => !slot.IsEmpty).Select(slot => slot.Item).ToArray();

        }

        public IInventoryItem[] GetAllTypes(InventoryItemType itemType) {
            return Slots.Where(slot => !slot.IsEmpty && slot.Item.ItemType == itemType).Select(slot => slot.Item).ToArray();

        }

        public IInventoryItem[] GetEquippedItems() {
            return Slots.Where(slot => !slot.IsEmpty && slot.Item.State.IsEquipped).Select(slot => slot.Item).ToArray();
        }

        public int GetItemAmount(InventoryItemType itemType) {
            return Slots.Where(slot => !slot.IsEmpty && slot.Item.ItemType == itemType).Sum(slot => slot.GetItemAmount);
        }

        // public bool Add() {
        //
        // }
        // public bool AddToSlot() {
        //
        // }

        public bool TryToAdd(object sender, IInventoryItem item) {
            // Поиск слота с тем же типом предмета, который не пуст и не полон
            var slotWithSameItemButNotEmpty = Slots
                .FirstOrDefault(slot => !slot.IsEmpty && !slot.IsFull && slot.Item.ItemType == item.ItemType);

            if (slotWithSameItemButNotEmpty != null)
                return TryAddToSlot(sender, slotWithSameItemButNotEmpty, item);

            // Поиск пустого слота
            var emptySlot = Slots.FirstOrDefault(slot => slot.IsEmpty);
            if (emptySlot != null)
                return TryAddToSlot(sender, emptySlot, item);

            Debug.Log("Нет места для предметов в инвентаре!!!");
            return false;
        }

        public bool TryAddToSlot(object sender, IInventorySlot slot, IInventoryItem item) {
            Debug.Log($"IInventory slot is null : {slot==null}");
            Debug.Log($"IInventory item is null : {item==null}");
            Debug.Log($"IInventory item Info is null : {item.Info==null} and ItemInfo = {item.Info.ItemType.ToString()}");
            if (item == null) return false;
            var fits = slot.GetItemAmount + item.State.Amount <= item.Info.MaxAmountSlot;
            var amountToAdd = fits
                ? item.State.Amount
                : item.Info.MaxAmountSlot - slot.GetItemAmount;
            var amountLeft = item.State.Amount - amountToAdd;
            var clonedItem = new InventoryItem(item.Info);
            clonedItem.Amount =amountToAdd;
            
            if (slot.IsEmpty) {
                
                slot.Item=clonedItem;
                Debug.Log($"slot.Item.State.GetItemAmount === {slot.Item.State.Amount}");
                
                Debug.Log("Setup Item!");
            } else {
                slot.Item.State.Amount +=amountToAdd;
                Debug.Log("Added Item!");
            }


            Debug.Log($"Item added to _inventory. InventoryItemType: {item.ItemType}, GetItemAmount: {amountToAdd}");
            OnInventoryItemAddedEvent?.Invoke(sender, item, amountToAdd);
            OnInventoryStateChangedEvent?.Invoke(sender);

            if (amountLeft <= 0)
                return true;
            //if (IsFull) return true;

            item.Amount=amountLeft;
            return TryToAdd(sender, item);
        }


        public void TransitFromSlotToSlot(object sender, IInventorySlot fromSlot, IInventorySlot toSlot) {
            if (fromSlot.IsEmpty) {
                DeselectSelectedSlot();
                return;
            }
            if (!toSlot.IsEmpty && fromSlot.ItemType != toSlot.ItemType) {

                var tempItemTo = new InventoryItem(toSlot.Item.Info);
                var temAmountTo = toSlot.GetItemAmount;
                toSlot.Clear();
                toSlot.Item =new InventoryItem(fromSlot.Item.Info);
                toSlot.Item.Amount=fromSlot.GetItemAmount;
                fromSlot.Clear();
                fromSlot.Item =tempItemTo;
                fromSlot.Item.Amount=temAmountTo;
                DeselectSelectedSlot();
                Debug.Log("Обмен слоатми разных типов!");
                return;
            }

            
            if (toSlot.IsEmpty) {
                toSlot.Item =new InventoryItem(fromSlot.Item.Info);
                toSlot.Item.Amount=fromSlot.GetItemAmount;
                fromSlot.Clear();
                DeselectSelectedSlot();
                return;
            }

            if (fromSlot == toSlot) {
                DeselectSelectedSlot();
                return;
            }

            if (fromSlot.ItemType == toSlot.ItemType) {
                var slotCapacity = fromSlot.Item.Info.MaxAmountSlot;
                //Поместятся ли все предметы
                var fits = fromSlot.GetItemAmount + toSlot.GetItemAmount <= slotCapacity;
                //Если да, то добавляем GetItemAmount, если не помещаются, то определим остаток
                var amountToAdd = fits ? fromSlot.GetItemAmount : slotCapacity - toSlot.GetItemAmount;
                //Запишем остаток в переменную
                var amountLeft = fromSlot.GetItemAmount - amountToAdd;
                toSlot.Item.State.Amount +=amountToAdd;
                if (fits) {
                    fromSlot.Clear();
                } else {
                    fromSlot.Item.Amount = amountLeft;
                }
                DeselectSelectedSlot();
                return;
            }
            

        }


        public void Remove(object sender, InventoryItemType itemType, int amount = 1) {
            var slotsWithItem = GetAllSlots(itemType);
            if (slotsWithItem.Length == 0) return;

            var amountToRemove = amount;

            foreach (var slot in slotsWithItem) {
                if (amountToRemove <= 0) break;

                if (slot.GetItemAmount >= amountToRemove) {
                    slot.Item.State.Amount -=amountToRemove;
                    if (slot.GetItemAmount <= 0) slot.Clear();

                    Debug.Log($"Item removed from _inventory. InventoryItemType: {itemType}, GetItemAmount: {amountToRemove}");
                    OnInventoryItemRemovedEvent?.Invoke(sender, itemType, amountToRemove);
                    OnInventoryStateChangedEvent?.Invoke(sender);
                    break;
                } else {
                    amountToRemove -= slot.GetItemAmount;
                    Debug.Log($"Item removed from _inventory. InventoryItemType: {itemType}, GetItemAmount: {slot.GetItemAmount}");
                    OnInventoryItemRemovedEvent?.Invoke(sender, itemType, slot.GetItemAmount);
                    slot.Clear();
                    OnInventoryStateChangedEvent?.Invoke(sender);
                }
            }
        }


        public bool HasItem(InventoryItemType itemType, out IInventoryItem item) {
            item = GetItem(itemType);
            return item != null;
        }


        public IInventorySlot[] GetAllSlots(InventoryItemType itemType) {
            return Slots.Where(slot => !slot.IsEmpty && slot.Item.ItemType == itemType).ToArray();
        }

        public IInventorySlot[] GetAllSlots() {
            return Slots;
        }

        public IInventorySlot GetSlotByIndex(int index) {
            if (index < 0 || index >= Slots.Length) {
                throw new ArgumentOutOfRangeException(nameof(index), "Индекс вне диапазона слотов инвентаря.");
            }
            return Slots[index];
        }
    }
}
