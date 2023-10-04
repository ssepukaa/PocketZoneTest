using System;
using Assets.Scripts.InventoryObject.Abstract;
using Assets.Scripts.InventoryObject.Data;
using Assets.Scripts.UI.Base;
using Assets.Scripts.UI.InventoryUI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.GameScene.Windows {
    public class UIInventoryWindow : UIBaseWindows {

        public TMP_Text DescriptionItemText;
        public Button DropItemButton;
        private UIInventory _inventory;
        private IInventoryItemInfo _inventoryItemInfo;
       


        private void Awake() {
            _inventory = GetComponentInParent<UIInventory>();
            idUIWindowsType = UIWindowsType.Inventory;
            DropItemButton.gameObject.SetActive(false);
        }

        private void OnEnable() {
            Time.timeScale = 0f;

        }

        private void OnDisable() {
            Time.timeScale = 1f;
        }


        public void OnCloseButton() {
            _controller.ShowWindow(UIWindowsType.HUD);

        }

        public void OnDropButton() {
            _inventory.OnRemoveItemButton();
        }

        public void UpdateUI(IInventorySlot slot) {
            if (slot == null) {
                DescriptionItemText.text = String.Empty;
                DropItemButton.gameObject.SetActive(false);

            } else {
                _inventoryItemInfo = slot.Item.Info;
                DescriptionItemText.text = _inventoryItemInfo.Description;
                DropItemButton.gameObject.SetActive(true);
            }


        }
    }
}