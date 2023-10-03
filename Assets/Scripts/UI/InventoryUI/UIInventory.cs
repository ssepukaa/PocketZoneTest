using System.Collections.Generic;
using Assets.Scripts.InventoryObject;
using Assets.Scripts.InventoryObject.Abstract;
using Assets.Scripts.InventoryObject.Data;
using Assets.Scripts.InventoryObject.Items;
using Assets.Scripts.Player;
using Assets.Scripts.UI.GameScene.Windows;
using UnityEngine;

namespace Assets.Scripts.UI.InventoryUI {
    public class UIInventory : MonoBehaviour {

        private PlayerController _player;
        public GameObject slotPrefab; // Drag and drop your SlotPrefab here in the inspector
        public RectTransform slotsContainer; // Parent object for all slots
        private UIInventoryWindow _inventoryWindow;

        [SerializeField] private ItemsInfoDataBase _itemsInfoDataBase;
        private IInventoryItem item1;
        private IInventoryItem item2;
        private IInventoryItem item3;
        private IInventoryItem item4;


       // private Inventory _inventory;
      
        public UIInventorySlot[] _uiSlots;
        //public event Action<int> OnSlotSelected; // Событие, которое вызывается при выборе слота
        private void OnEnable() {
          
        }
        
        private void OnDestroy() {
            _player.rd.Inventory.OnInventoryStateChangedEvent -= OnInventoryChanged;
           
        }


        public void Construct(PlayerController playerController) {
            _player = playerController;
            _itemsInfoDataBase = _player.rd.inventoryInfosData;
            _inventoryWindow = GetComponentInChildren<UIInventoryWindow>();
            SetItems();
            
            AddItems();
            InitializeSlots();
            UpdateUI();
            
            _player.rd.Inventory.OnInventoryStateChangedEvent += OnInventoryChanged;
           
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

        private void InitializeSlots() {
            _uiSlots = new UIInventorySlot[_player.rd.CapacityInventory];
            for (int i = 0; i < _player.rd.CapacityInventory; i++) {
                GameObject slotGO = Instantiate(slotPrefab, slotsContainer);
                UIInventorySlot uiSlot = slotGO.GetComponent<UIInventorySlot>();
                _uiSlots[i] = uiSlot;
                _uiSlots[i].Construct(this, _player.rd.Inventory.GetSlotByIndex(i), i);
                Debug.Log($"_player.rd.Inventory.GetSlotByIndex(i).IsEmpty ====={_player.rd.Inventory.GetSlotByIndex(i).IsEmpty} and number of slot = {i}");
            }


            
        }

        private void AddItems() {
            Debug.Log($"UIInventory:   _player.rd.Inventory == null {_player.rd.Inventory == null}");

            _player.rd.Inventory.TryAddToSlot(this, _player.rd.Inventory.GetSlotByIndex(0), item1);
            _player.rd.Inventory.TryAddToSlot(this, _player.rd.Inventory.GetSlotByIndex(1), item2);
            _player.rd.Inventory.TryAddToSlot(this, _player.rd.Inventory.GetSlotByIndex(2), item3);
            _player.rd.Inventory.TryAddToSlot(this, _player.rd.Inventory.GetSlotByIndex(3), item4);
        }

        public void UpdateUI() {
            foreach (var uiSlot in _uiSlots) {
                uiSlot.Refresh();
            }
        }

        private void OnInventoryChanged(object sender) {
            UpdateUI();
        }
        
        public void OnSlotButton(IInventorySlot slot) {
            _player.rd.Inventory.ButtonSlotSelected(slot);
            
            _inventoryWindow.UpdateUI(_player.rd.Inventory.GetSelectedSlot());
        }

    }
}
