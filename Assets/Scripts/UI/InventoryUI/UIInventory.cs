using Assets.Scripts.InventoryObject;
using Assets.Scripts.InventoryObject.Abstract;
using Assets.Scripts.InventoryObject.Data;

using Assets.Scripts.Player;
using Assets.Scripts.Player.Abstract;
using Assets.Scripts.UI.GameScene.Windows;
using UnityEngine;

namespace Assets.Scripts.UI.InventoryUI {
    public class UIInventory : MonoBehaviour {
        public GameObject slotPrefab; // Drag and drop your SlotPrefab here in the inspector
        public RectTransform slotsContainer; // Parent object for all slots

        IPlayerController _player;
        IInventoryItem item1;
        IInventoryItem item2;
        IInventoryItem item3;
        IInventoryItem item4;
        UIInventoryWindow _inventoryWindow;
        [SerializeField] ItemsInfoDataBase _itemsInfoDataBase;


        // private Inventory _inventory;

        public UIInventorySlot[] _uiSlots;
        //public event Action<int> OnSlotSelected; // Событие, которое вызывается при выборе слота
        private void OnEnable() {

        }

        public void Construct(PlayerController playerController) {
            _player = playerController;
            //_itemsInfoDataBase = _player.RD.inventoryInfosData;
            _inventoryWindow = GetComponentInChildren<UIInventoryWindow>();
            // SetItems();

            // AddItems();
            InitializeSlots();
            UpdateUI();

            _player.Inventory.OnInventoryStateChangedEvent += OnInventoryChanged;
            _player.Inventory.OnOneItemAmmoRemovedEvent -= OnOneItemAmmoRemoved;

        }

        private void OnDestroy() {
            _player.Inventory.OnInventoryStateChangedEvent -= OnInventoryChanged;
            _player.Inventory.OnOneItemAmmoRemovedEvent -= OnOneItemAmmoRemoved;


        }

        private void OnOneItemAmmoRemoved(object sender, IInventoryItem item, int amount, int totalAmountAmmo) {

        }


        private void InitializeSlots() {
            _uiSlots = new UIInventorySlot[_player.Inventory.Capacity];
            for (int i = 0; i < _player.Inventory.Capacity; i++) {
                GameObject slotGO = Instantiate(slotPrefab, slotsContainer);
                UIInventorySlot uiSlot = slotGO.GetComponent<UIInventorySlot>();
                _uiSlots[i] = uiSlot;
                _uiSlots[i].Construct(this, _player.Inventory.GetSlotByIndex(i), i);
            }



        }

        public void UpdateUI() {
            foreach (var uiSlot in _uiSlots) {
                uiSlot.Refresh();

            }
            _inventoryWindow.UpdateUI(_player.Inventory.GetSelectedSlot());
        }

        private void OnInventoryChanged(object sender) {
            UpdateUI();


        }

        public void OnSlotButton(IInventorySlot slot) {
            _player.Inventory.ButtonSlotSelected(slot);
            UpdateUI();

        }

        public void OnDropItemButton() {
            _player.Inventory.RemoveOneAmountItemInSelectedSlotDropped(this);
            UpdateUI();
        }

        public void OnEquipItemButton() {
            _player.Inventory.EquipItem(this);
        }

        private void SetItems() {
            item1 = new InventoryItem(_itemsInfoDataBase.AmmoInfo);
            item1.Amount=3;
            item3 = new InventoryItem(_itemsInfoDataBase.AmmoInfo);
            item3.Amount = 3;
            item2 = new InventoryItem(_itemsInfoDataBase.RifleInfo);
            item2.Amount = 2;
            item4 = new InventoryItem(_itemsInfoDataBase.RifleInfo);
            item4.Amount = 2;

        }

        private void AddItems() {
            Debug.Log($"UIInventory:   Player.RD.Inventory == null {_player.Inventory == null}");

            _player.Inventory.TryAddToSlot(this, _player.Inventory.GetSlotByIndex(0), item1);
            _player.Inventory.TryAddToSlot(this, _player.Inventory.GetSlotByIndex(1), item2);
            _player.Inventory.TryAddToSlot(this, _player.Inventory.GetSlotByIndex(2), item3);
            _player.Inventory.TryAddToSlot(this, _player.Inventory.GetSlotByIndex(3), item4);
        }
    }
}
