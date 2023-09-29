using Assets.Scripts.InventoryObject;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.InventoryUI {
    public class UIInventorySlot : MonoBehaviour {
        public int SlotIndex;
        public Color selectedColor = Color.yellow; // цвет при выделении
        public Color defaultColor = Color.white; // стандартный цвет
        private InventorySlot _slot;
        private UIInventory _uiInventory;
        private bool _isSelected = false; // флаг, указывающий, выделен ли слот
        private Image slotImage; // ссылка на компонент Image
        [SerializeField] private UIInventoryItem _uiInventoryItem;


        private void Awake() {
          
            slotImage = GetComponent<Image>(); // получаем компонент Image
            _uiInventoryItem = GetComponentInChildren<UIInventoryItem>();
        }


        public void Construct(UIInventory uiInventory, int slotIndex) {
            _uiInventory = uiInventory;
            SlotIndex = slotIndex;
        }

        public void OnSlotButtonClicked() {
            Debug.Log($"Click slot number{SlotIndex}");
            _uiInventory.OnSlotButton(SlotIndex);
            
        }

        public void Select() {
            _isSelected = true;
            slotImage.color = selectedColor;
        }

        public void Deselect() {
            _isSelected = false;
            slotImage.color = defaultColor;
        }


        public void Refresh() {
            _slot = _uiInventory.GetInventory().GetSlotByIndex(SlotIndex);
            if (_slot != null) {
                _uiInventoryItem.Refresh(_slot);
            }
        }
    }
}


//
// using Assets.Scripts.Inventory;
// using Assets.Scripts.Inventory.Abstract;
// using UnityEngine;
// using UnityEngine.EventSystems;
//
// namespace Assets.Scripts.UI.Inventory {
//     public class UIInventorySlot : MonoBehaviour, IDropHandler {
//         [SerializeField] private UIInventoryItem _uiInventoryItem;
//         public IInventorySlot slot { get; private set; }
//         private IInventorySlot _inventorySlot;
//         private UIInventory UIInventory;
//
//         private void Awake() {
//             UIInventory = GetComponentInParent<UIInventory>();
//         }
//
//         public void Initialize(IInventorySlot inventorySlot, UIInventory uiInventory) {
//             _inventorySlot = inventorySlot;
//             UIInventory = uiInventory;
//             Refresh();
//         }
//
//
//         public virtual void OnDrop(PointerEventData eventData) {
//             if (eventData.pointerDrag == null) return;
//
//             var otherItemUI = eventData.pointerDrag.GetComponent<UIInventoryItem>();
//             if (otherItemUI == null) return;
//
//             var otherSlotUI = otherItemUI.GetComponentInParent<UIInventorySlot>();
//             if (otherSlotUI == null) return;
//
//             var otherSlot = otherSlotUI.slot;
//             if (otherSlot == null) return;
//             var otherItemTransform = eventData.pointerDrag.transform;
//             otherItemTransform.SetParent(transform);
//             otherItemTransform.localPosition = Vector3.zero;
//         
//             /*var otherItemUI = eventData.pointerDrag.GetComponent<UIInventoryItem>();
//             var otherSlotUI = otherItemUI.GetComponentInParent<UIInventorySlot>();
//             var otherSlot = otherSlotUI.slot;*/
//             var _inventory = UIInventory._inventory;
//             _inventory.TransitFromSlotToSlot(this, otherSlot,slot);
//             Refresh();
//             otherSlotUI.Refresh();
//         }
//
//         public void Refresh() {
//             
//
//             if (slot != null) {
//                 _uiInventoryItem.Refresh(slot);
//                 
//
//             }
//         }
//     }
// }