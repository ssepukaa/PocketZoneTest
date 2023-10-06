using Assets.Scripts.InventoryObject;
using Assets.Scripts.InventoryObject.Abstract;
using Assets.Scripts.InventoryObject.Data;

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
            _player.RD.Inventory.OnInventoryStateChangedEvent -= OnInventoryChanged;
            _player.RD.Inventory.OnOneItemAmmoRemovedEvent -= OnOneItemAmmoRemoved;
            

        }

        private void OnOneItemAmmoRemoved(object sender, IInventoryItem item, int amount = 1) {
            
        }


        public void Construct(PlayerController playerController) {
            _player = playerController;
            //_itemsInfoDataBase = _player.RD.inventoryInfosData;
            _inventoryWindow = GetComponentInChildren<UIInventoryWindow>();
           // SetItems();
            
           // AddItems();
            InitializeSlots();
            UpdateUI();
            
            _player.RD.Inventory.OnInventoryStateChangedEvent += OnInventoryChanged;
            _player.RD.Inventory.OnOneItemAmmoRemovedEvent -= OnOneItemAmmoRemoved;

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
            _uiSlots = new UIInventorySlot[_player.RD.CapacityInventory];
            for (int i = 0; i < _player.RD.CapacityInventory; i++) {
                GameObject slotGO = Instantiate(slotPrefab, slotsContainer);
                UIInventorySlot uiSlot = slotGO.GetComponent<UIInventorySlot>();
                _uiSlots[i] = uiSlot;
                _uiSlots[i].Construct(this, _player.RD.Inventory.GetSlotByIndex(i), i);
            }


            
        }

        private void AddItems() {
            Debug.Log($"UIInventory:   Player.RD.Inventory == null {_player.RD.Inventory == null}");

            _player.RD.Inventory.TryAddToSlot(this, _player.RD.Inventory.GetSlotByIndex(0), item1);
            _player.RD.Inventory.TryAddToSlot(this, _player.RD.Inventory.GetSlotByIndex(1), item2);
            _player.RD.Inventory.TryAddToSlot(this, _player.RD.Inventory.GetSlotByIndex(2), item3);
            _player.RD.Inventory.TryAddToSlot(this, _player.RD.Inventory.GetSlotByIndex(3), item4);
        }

        public void UpdateUI() {
            foreach (var uiSlot in _uiSlots) {
                uiSlot.Refresh();
                
            }
            _inventoryWindow.UpdateUI(_player.RD.Inventory.GetSelectedSlot());
        }

        private void OnInventoryChanged(object sender) {
            UpdateUI();


        }
        
        public void OnSlotButton(IInventorySlot slot) {
            _player.RD.Inventory.ButtonSlotSelected(slot);
            UpdateUI();

        }

        public void OnDropItemButton() {
            _player.RD.Inventory.RemoveOneAmountItemInSelectedSlotDropped(this);
            UpdateUI();
        }

        public void OnEquipItemButton() {
            _player.RD.Inventory.EquipItem(this);
        }
    }
}
