using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.InventoryObject.Abstract;
using Assets.Scripts.InventoryObject.Data;
using Assets.Scripts.Player.Data;
using UnityEngine;

namespace Assets.Scripts.InventoryObject {
    public class Inventory : IInventory {
        public event Action<object, IInventoryItem, int> OnInventoryItemAddedEvent;
        public event Action<object, InventoryItemType, int> OnInventoryItemRemovedEvent;
        public event Action<object> OnInventoryStateChangedEvent;
        public event Action<object, IInventoryItemInfo, int> OnOneItemInSelectedSlotDroppedEvent;
        public event Action<object, IInventoryItemInfo, int> OnRemoveOneAmountItemInSelectedSlotEquippedEvent;
        public event Action<object, IInventoryItem, int, int> OnOneItemAmmoRemovedEvent;
        public event Action OnAmmoChangedEvent;

        public int Capacity { get; set; }
        // public bool IsFull => SlotsArray.All(slot => slot.IsFull);

        public IInventorySlot WeaponSlot { get => _data._weaponSlot; set => _data._weaponSlot = value; }

        public IInventorySlot[] SlotsArray { get => _data._slots; set => _data._slots = value; }

        public IInventorySlot ClipSlot { get => _data._clipSlot; set => _data._clipSlot = value; }

        PlayerModelData _data;
        IInventorySlot _selectedSlot;


        public Inventory(int capacity, PlayerModelData data) {
            _data = data;
            Capacity = capacity;
            InitializeSlots(capacity);

        }

        private void InitializeSlots(int capacity) {
            WeaponSlot = new InventorySlot();
            _selectedSlot =new InventorySlot();
            DeselectSelectedSlot();

            SlotsArray = new InventorySlot[capacity];
            for (int i = 0; i < capacity; i++) {
                SlotsArray[i] = new InventorySlot();
            }
        }

        public void ButtonSlotSelected(IInventorySlot slot) {
            SelectSlot(slot);
        }

        // Выбор слота 
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
                OnAmmoChangedEvent?.Invoke();

                return;
            }
            Debug.Log("Никакие условия не выполнены!!");

        }

        private void DeselectSelectedSlot() {
            if (_selectedSlot != null) {
                _selectedSlot.Deselect();
                _selectedSlot = null;
            }

            OnInventoryStateChangedEvent?.Invoke(this);
            OnAmmoChangedEvent?.Invoke();

        }


        //Добавление предмета в инвентарь
        public bool TryToAdd(object sender, IInventoryItem item) {
            while (item.State.Amount > 0) {
                // Поиск всех слотов с тем же типом предмета, которые не полны
                var suitableSlots = SlotsArray
                    .Where(slot => (slot.IsEmpty || (slot.Item.ItemType == item.ItemType && slot.Item.Amount<= slot.Item.Info.MaxAmountSlot)))
                    .ToList();

                if (!suitableSlots.Any()) {
                    Debug.Log("Нет места для предметов в инвентаре!!!");
                    return false; // Если не найдено ни одного подходящего слота
                }

                bool itemAdded = false;

                foreach (var slot in suitableSlots) {
                    if (item.State.Amount <= 0) break; // Если весь предмет был добавлен
                    if (TryAddToSlot(sender, slot, item)) {
                        itemAdded = true;
                    }
                }

                if (!itemAdded) {
                    return false; // Если предмет не был добавлен ни в один из слотов
                }
            }
            OnAmmoChangedEvent?.Invoke();

            return true;
        }
        //Добавление предмета уже в конкретный слот
        public bool TryAddToSlot(object sender, IInventorySlot slot, IInventoryItem item) {
            var amountToAdd = Math.Min(item.Info.MaxAmountSlot - slot.GetItemAmount, item.State.Amount);

            if (slot.IsEmpty) {
                var clonedItem = new InventoryItem(item.Info) {
                    Amount = amountToAdd
                };
                slot.Item = clonedItem;
                if (WeaponSlot == null || WeaponSlot.IsEmpty) {
                    DeselectSelectedSlot();
                    SelectSlot(slot);
                    EquipItem(this);
                }
            } else {
                slot.Item.State.Amount += amountToAdd;
            }

            item.State.Amount -= amountToAdd;

            OnInventoryItemAddedEvent?.Invoke(sender, item, amountToAdd);
            OnOneItemAmmoRemovedEvent?.Invoke(sender, ClipSlot.Item, 1, GetItemAmount(ClipSlot.Item.ItemType));
            OnInventoryStateChangedEvent?.Invoke(sender);
            OnAmmoChangedEvent?.Invoke();
            return true;
        }



        //Перемещение предмета в слот
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

        // Экипировка выбранного слота

        public bool EquipItem(object sender) {

            if (_selectedSlot.IsEmpty || !_selectedSlot.IsSelect) {

                DeselectSelectedSlot();
                OnInventoryStateChangedEvent?.Invoke(sender);
                return false;
            }

            if (_selectedSlot.Item.Info.ItemEquippableType == ItemIsEquippableType.Equippable) {
                if (!WeaponSlot.IsEmpty) {

                    var tempItem = new InventoryItem(WeaponSlot.Item.Info);
                    tempItem.Amount = 1;
                    WeaponSlot.Clear();
                    WeaponSlot.Item =new InventoryItem(_selectedSlot.Item.Info);
                    WeaponSlot.Item.Amount= 1;

                    RemoveOneAmountItemInSelectedSlotEquipped(this);
                    TryToAdd(this, tempItem);

                } else {
                    WeaponSlot.Item =new InventoryItem(_selectedSlot.Item.Info);
                    WeaponSlot.Item.Amount= 1;

                    RemoveOneAmountItemInSelectedSlotEquipped(this);

                }
                ClipSlot.Item = new InventoryItem(WeaponSlot.Item.Info);
                ClipSlot.Item.Amount = 0;
                ReloadClipSlot();
            }


            DeselectSelectedSlot();
            return true;

        }
        // Удаление 1 единицы предмета из первого слота - не использовал

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
        // Удаление одной единицы предмета из выбранного слота по нажатию кнопки Выбросить

        public bool RemoveOneAmountItemInSelectedSlotDropped(object sender) {
            if (_selectedSlot.IsEmpty || !_selectedSlot.IsSelect || _selectedSlot.GetItemAmount <= 0) {
                Debug.Log("SelectedSlot Null!!!");
                OnInventoryStateChangedEvent?.Invoke(sender);
                OnAmmoChangedEvent?.Invoke();

                return false;
            }
            _selectedSlot.Item.State.Amount -= 1;
            OnOneItemInSelectedSlotDroppedEvent?.Invoke(sender, _selectedSlot.Item.Info, 1);
            if (_selectedSlot.GetItemAmount <= 0) {
                _selectedSlot.Clear();
                DeselectSelectedSlot();
            }
            OnInventoryStateChangedEvent?.Invoke(sender);
            OnAmmoChangedEvent?.Invoke();

            OnOneItemAmmoRemovedEvent?.Invoke(sender, ClipSlot.Item, 1, GetItemAmount(ClipSlot.Item.ItemType));
            return true;
        }
        // Удаление из инвентаря экипированного предмета

        public bool RemoveOneAmountItemInSelectedSlotEquipped(object sender) {
            if (_selectedSlot.IsEmpty || !_selectedSlot.IsSelect || _selectedSlot.GetItemAmount <= 0) {
                Debug.Log("SelectedSlot Null!!!");
                OnInventoryStateChangedEvent?.Invoke(sender);
                OnAmmoChangedEvent?.Invoke();

                return false;
            }
            _selectedSlot.Item.State.Amount -= 1;
            OnRemoveOneAmountItemInSelectedSlotEquippedEvent?.Invoke(sender, _selectedSlot.Item.Info, 1);
            if (_selectedSlot.GetItemAmount <= 0) {
                _selectedSlot.Clear();
                DeselectSelectedSlot();
            }
            OnOneItemAmmoRemovedEvent?.Invoke(sender, ClipSlot.Item, 1, GetItemAmount(ClipSlot.Item.ItemType));
            OnInventoryStateChangedEvent?.Invoke(sender);
            OnAmmoChangedEvent?.Invoke();

            return true;
        }
        // Удаление одного патрона из слота с боеприпасами подходящими для данного оружия

        public bool RemoveOneAmountItemInAmmoByType(object sender, ItemAmmoType ammoType) {
            IInventorySlot slot = GetAmmoSlotByType(ammoType);

            if (slot == null || slot.IsEmpty || slot.Item.Info.FunctionalityType != ItemFunctionalityType.Ammo) {
                OnInventoryStateChangedEvent?.Invoke(sender);

                return false;
            }
            slot.Item.State.Amount -= 1;
            OnOneItemAmmoRemovedEvent?.Invoke(sender, slot.Item, 1, GetItemAmount(slot.Item.ItemType));
            if (slot.Item.State.Amount <= 0) {
                slot.Clear();
            }
            OnInventoryStateChangedEvent?.Invoke(sender);
            OnAmmoChangedEvent?.Invoke();


            return true;
        }
        // Выстрел из оружия и удаление патрона из слота обоймы

        public bool ShootingAndRemoveAmmo(object sender) {
            if (ClipSlot == null || ClipSlot.Item == null) return false;
            if (ClipSlot.Item.Amount <=0) {
                //ReloadClipSlot();
                Debug.Log("No ammo!");
                OnInventoryStateChangedEvent?.Invoke(sender);
                OnAmmoChangedEvent?.Invoke();

                return false;
            }
            ClipSlot.Item.State.Amount -= 1;
            OnInventoryStateChangedEvent?.Invoke(sender);
            OnAmmoChangedEvent?.Invoke();

            return true;
        }
        // Перезарядка магазина

        public bool ReloadClipSlot() {
            Debug.Log("Start Reloading");
            OnAmmoChangedEvent?.Invoke();
            // Проверяем, полон ли магазин
            if (ClipSlot.Item.Amount >= ClipSlot.Item.Info.WeaponInfo.CapacityClip) {
                Debug.Log("Break Reloading - clip is full");
                return false; // Магазин уже полон
            }

            // Получаем тип патронов для текущего оружия
            var ammoType = WeaponSlot.Item.Info.AmmoInfo.AmmoType;

            // Получаем все слоты с патронами нужного типа
            var ammoSlots = GetAmmoSlotsByType(ammoType);
            if (ammoSlots == null) {
                Debug.Log("Break Reloading - no clips  for reload");
                OnInventoryStateChangedEvent?.Invoke(this);
                OnAmmoChangedEvent?.Invoke();

                return false;
            }
            // Пока магазин не полон
            foreach (var ammoSlot in ammoSlots) {

                // Вычисляем, сколько патронов можно добавить в магазин
                var amountToAdd = Math.Min(ammoSlot.Item.Amount, WeaponSlot.Item.Info.WeaponInfo.CapacityClip - ClipSlot.Item.Amount);

                // Добавляем патроны в магазин
                ClipSlot.Item.Amount += amountToAdd;
                Debug.Log($"ClipSlot Add  {amountToAdd} ammo");
                // Убираем патроны из слота
                ammoSlot.Item.Amount -= amountToAdd;
                if (ammoSlot.Item.Amount == 0) {
                    Debug.Log("Slot Empty After Reloading");
                    ammoSlot.Clear();

                }
                //OnOneItemAmmoRemovedEvent?.Invoke(this, ClipSlot.Item, 1, GetItemAmount(ClipSlot.Item.ItemType));
                OnInventoryStateChangedEvent?.Invoke(this);
                OnAmmoChangedEvent?.Invoke();

                // Если магазин полон, выходим из цикла
                if (ClipSlot.Item.Amount >= ClipSlot.Item.Info.WeaponInfo.CapacityClip) {
                    Debug.Log("ClipSlot Full After Reloading - return true !!!!");
                    //OnOneItemAmmoRemovedEvent?.Invoke(this, ClipSlot.Item, 1, GetItemAmount(ClipSlot.Item.ItemType));
                    OnInventoryStateChangedEvent?.Invoke(this);
                    OnAmmoChangedEvent?.Invoke();


                    return true;
                }
            }
            Debug.Log("ClipSlot Not Full After Reloading - return false !!!!");
            OnInventoryStateChangedEvent?.Invoke(this);
            OnAmmoChangedEvent?.Invoke();
            return false;

        }


        // Получение колчиества патронов по типу боеприпаса

        public int GetTotalAmmoByType(ItemAmmoType itemType) {
            return SlotsArray.Where(slot => slot != null
                                       && slot.Item != null
                                       && slot.Item.Info != null
                                       && slot.Item.Info.AmmoInfo != null
                                       && slot.Item.Info.ItemType == InventoryItemType.Ammo
                                       && slot.Item.Info.AmmoInfo.AmmoType == itemType)
                .Sum(slot => slot.Item.Amount); // Суммируем количество патронов в каждом слоте
        }

        // Получение всех слотов с патронами определенного типа

        public List<IInventorySlot> GetAmmoSlotsByType(ItemAmmoType itemType) {
            return SlotsArray.Where(slot => slot != null
                                       && slot.Item != null
                                       && slot.Item.Info != null
                                       && slot.Item.Info.AmmoInfo != null
                                       && slot.Item.Info.ItemType == InventoryItemType.Ammo
                                       && slot.Item.Info.AmmoInfo.AmmoType == itemType).ToList();
        }




        // Выгрузка патронов из магазина при смене оружия - доделать
        public void UnloadWeaponAmmoToInventory() {
            if (WeaponSlot == null || WeaponSlot.IsEmpty) {
                Debug.Log("No weapon equipped.");
                return;
            }

            // Предположим, что у вашего IInventoryItem есть свойство AmmoType, которое указывает тип боеприпасов для оружия.
            var ammoType = WeaponSlot.Item.Info.WeaponInfo.AmmoType;

            // if (ammoType == null) {
            //     Debug.Log("This weapon does not use ammo.");
            //     return;
            // }

            // Находим слот в инвентаре, который может вместить этот тип боеприпасов.
            var ammoSlot = GetAmmoSlotByType(ammoType);

            if (ammoSlot == null) {
                Debug.Log($"No slot found for ammo type: {ammoType}");
                return;
            }

            // Перемещаем боеприпасы из слота оружия в слот инвентаря.
            TransitFromSlotToSlot(this, WeaponSlot, ammoSlot);
        }


        public bool HasItem(InventoryItemType itemType, out IInventoryItem item) {
            item = GetItem(itemType);
            return item != null;
        }


        public IInventorySlot[] GetAllSlots(InventoryItemType itemType) {
            return SlotsArray.Where(slot => !slot.IsEmpty && slot.Item.ItemType == itemType).ToArray();
        }

        public IInventorySlot[] GetAllSlots() {
            return SlotsArray;
        }

        public IInventorySlot GetSlotByIndex(int index) {
            if (index < 0 || index >= SlotsArray.Length) {
                throw new ArgumentOutOfRangeException(nameof(index), "Индекс вне диапазона слотов инвентаря.");
            }
            return SlotsArray[index];
        }
        public int GetIndexOfSelectedSlot(IInventorySlot selectedSlot) {
            return Array.IndexOf(SlotsArray, selectedSlot);
        }

        public IInventorySlot GetSelectedSlot() {
            return SlotsArray.FirstOrDefault(slot => slot.IsSelect);
        }

        public IInventoryItem GetItem(InventoryItemType itemType) {
            return SlotsArray.FirstOrDefault(slot => slot.ItemType == itemType)?.Item;

        }
        // Получение любого слота боеприпасов по типу боеприпасов
        public IInventorySlot GetAmmoSlotByType(ItemAmmoType itemType) {
            return SlotsArray.FirstOrDefault(slot => slot != null
                                                && slot.Item != null
                                                && slot.Item.Info != null
                                                && slot.Item.Info.AmmoInfo != null
                                                && slot.Item.Info.ItemType == InventoryItemType.Ammo
                                                && slot.Item.Info.AmmoInfo.AmmoType == itemType);
        }


        public IInventoryItem[] GetAllItems() {
            return SlotsArray.Where(slot => !slot.IsEmpty).Select(slot => slot.Item).ToArray();

        }

        public IInventoryItem[] GetAllTypes(InventoryItemType itemType) {
            return SlotsArray.Where(slot => !slot.IsEmpty
                                       && slot.Item.ItemType == itemType).Select(slot => slot.Item).ToArray();

        }

        public IInventoryItem[] GetEquippedItems() {
            return SlotsArray.Where(slot => !slot.IsEmpty
                                       && slot.Item.State.IsEquipped).Select(slot => slot.Item).ToArray();
        }

        public int GetItemAmount(InventoryItemType itemType) {
            return SlotsArray.Where(slot => !slot.IsEmpty
                                       && slot.Item.ItemType == itemType).Sum(slot => slot.GetItemAmount);
        }
    }
}

