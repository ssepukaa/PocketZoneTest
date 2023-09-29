using System.Collections.Generic;
using Assets.Scripts.InventoryObject;
using Assets.Scripts.InventoryObject.Data;
using Assets.Scripts.InventoryObject.Items;
using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.UI.InventoryUI {
    public class UIInventory : MonoBehaviour {

        private PlayerController _player;
        public GameObject slotPrefab; // Drag and drop your SlotPrefab here in the inspector
        public RectTransform slotsContainer; // Parent object for all slots

        [SerializeField] private InventoryItemInfo _appleInfo;
        [SerializeField] private InventoryItemInfo _pepperInfo;
        private Apple item1;
        private Pepper item2;
        private Apple item3;
        private Pepper item4;


        private Inventory _inventory;
        public List<UIInventorySlot> _uiSlots = new List<UIInventorySlot>();
        //public event Action<int> OnSlotSelected; // Событие, которое вызывается при выборе слота
        private void OnEnable() {
          
        }
        
        private void OnDisable() {
            _inventory.OnInventoryStateChangedEvent -= OnInventoryChanged;
        }


        public void Construct(PlayerController playerController) {
            _inventory = playerController.GetInventory();
            InitializeSlots();
            SetItems();
            UpdateUI();
            _inventory.OnInventoryStateChangedEvent += OnInventoryChanged;
        }

        private void SetItems() {
            item1 = new Apple(_appleInfo);
            item1.SetAmount(3);
            item3 = new Apple(_appleInfo);
            item3.SetAmount(3);
            item2 = new Pepper(_pepperInfo);
            item2.SetAmount(2);
            item4 = new Pepper(_pepperInfo);
            item4.SetAmount(2);
            _inventory.TryAddToSlot(this,_inventory.GetSlotByIndex(0), item1);
            _inventory.TryAddToSlot(this, _inventory.GetSlotByIndex(1), item2);
            _inventory.TryAddToSlot(this, _inventory.GetSlotByIndex(2), item3);
            _inventory.TryAddToSlot(this, _inventory.GetSlotByIndex(3), item4);
        }

        private void InitializeSlots() {
            for (int i = 0; i < _inventory.Capacity; i++) {
                GameObject slotGO = Instantiate(slotPrefab, slotsContainer);
                UIInventorySlot uiSlot = slotGO.GetComponent<UIInventorySlot>();
               _uiSlots.Add(uiSlot);
               uiSlot.Construct(this, i);
            }
            
        }

        public Inventory GetInventory() {
            return _inventory;
        }
        public void UpdateUI() {
            foreach (var uiSlot in _uiSlots) {
                uiSlot.Refresh();
            }
        }
        private void OnInventoryChanged(object sender) {
            UpdateUI();
        }


        public void OnSlotButton(int slotIndex) {
            _inventory.ButtonSlotSelected(slotIndex);
        }

    }
}


// public class UIInventory : MonoBehaviour {
//
//     private PlayerController _player;
//     [SerializeField] private InventoryItemInfo _appleInfo;
//     [SerializeField] private InventoryItemInfo _pepperInfo;
//     private Apple item1;
//     private Pepper item2;
//     public InventoryObject.Inventory _inventory;
//     public UIInventorySlot[] uiSlots;
//     private InventorySlot[] _inventorySlots;
//
//
//     public void Construct(PlayerController playerController) {
//
//
//         _player = playerController;
//         _inventory = _player.GetInventory();
//         _inventory.OnInventoryStateChangedEvent += OnInventoryChanged;
//         _inventorySlots = _inventory.GetAllSlots();
//         item1 = new Apple(_appleInfo);
//         item1.SetAmount(3);
//         item2 = new Pepper(_pepperInfo);
//         item2.SetAmount(2);
//         uiSlots = GetComponentsInChildren<UIInventorySlot>();
//         var first = _inventorySlots[1];
//         var second = _inventorySlots[2];
//         var third = _inventorySlots[3];
//         var fourth = _inventorySlots[4];
//
//         _inventory.TryAddToSlot(this, first, item1);
//         _inventory.TryAddToSlot(this, second, item1);
//
//         _inventory.TryAddToSlot(this, third, item2);
//         _inventory.TryAddToSlot(this, fourth, item2);
//         // Инициализация UI слотов
//         foreach (var uiSlot in uiSlots) {
//             var correspondingInventorySlot = _inventory.GetSlotByIndex(Array.IndexOf(uiSlots, uiSlot));
//             uiSlot.Initialize(correspondingInventorySlot);
//         }
//     }
//     private void OnInventoryChanged(object sender) {
//         foreach (var uiSlot in uiSlots) {
//             uiSlot.Refresh();
//         }
//
//     }
//
//     private void OnDestroy() {
//         _inventory.OnInventoryStateChangedEvent -= OnInventoryChanged;
//     }
//
// }