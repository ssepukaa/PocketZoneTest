using Assets.Scripts.InventoryObject;
using Assets.Scripts.InventoryObject.Abstract;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.InventoryUI {
    public class UIInventorySlot : MonoBehaviour {
        public int SlotIndex;

        public Color selectedColor = Color.green; // цвет при выделении
        public Color defaultColor = Color.grey; // стандартный цвет
        private IInventorySlot _slot;
        private UIInventory _uiInventory;

        Image slotImage; // ссылка на компонент Image
        [SerializeField] UIInventoryItem _uiInventoryItem;


        private void Awake() {

            slotImage = GetComponent<Image>(); // получаем компонент Image
            _uiInventoryItem = GetComponentInChildren<UIInventoryItem>();
        }


        public void Construct(UIInventory uiInventory, IInventorySlot slot, int index) {
            _uiInventory = uiInventory;
            _slot = slot;
            SlotIndex = index;
        }

        public void OnSlotButtonClicked() {
            if (_slot.Item == null) {
                Debug.Log($"Click slot number{SlotIndex}. Slot empty!");
            } else {
                Debug.Log($"Click slot number{SlotIndex}. Item in Slot === {_slot.Item.Info.Id}");
            }
            _uiInventory.OnSlotButton(_slot);

        }

        private void SelectToggle(bool isSelected) {
            switch (isSelected) {
                case true:
                    //_isSelected = true;
                    slotImage.color = selectedColor;
                    break;
                case false:
                    //_isSelected = false;
                    slotImage.color = defaultColor;
                    break;
            }

        }

        // private void CheckEquippedItemImage() {
        //     if(_slot.IsEmpty) {return; }
        //
        //     if (_slot.Item.State.IsEquipped) {
        //
        //     }
        // }

        public void Refresh() {

            // if (_slot != null) {
            SelectToggle(_slot.IsSelect);
            //CheckEquippedItemImage();
            _uiInventoryItem.Refresh(_slot);
            // }
        }
    }
}
