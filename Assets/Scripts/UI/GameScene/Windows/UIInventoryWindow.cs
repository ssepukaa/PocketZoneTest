using System;
using Assets.Scripts.InventoryObject.Abstract;
using Assets.Scripts.InventoryObject.Data;
using Assets.Scripts.UI.Base;
using Assets.Scripts.UI.InventoryUI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.GameScene.Windows {
    // Реализация окна Инвентаря
    public class UIInventoryWindow : UIBaseWindows {

        public TMP_Text DescriptionItemText;
        public Button DropItemButton;
        public Button EquipItemButton;
        UIInventory _inventory;
        IInventoryItemInfo _inventoryItemInfo;



        private void Awake() {
            _inventory = GetComponentInParent<UIInventory>();
            idUIWindowsType = UIWindowsType.Inventory;
            DropItemButton.gameObject.SetActive(false);
            EquipItemButton.gameObject.SetActive(false);
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
            _inventory.OnDropItemButton();
        }

        public void OnEquipItemButton() {
            _inventory.OnEquipItemButton();
        }

        public void UpdateUI(IInventorySlot slot) {
            if (slot == null) {
                DescriptionItemText.text = String.Empty;
                DropItemButton.gameObject.SetActive(false);
                EquipItemButton.gameObject.SetActive(false);

            } else {
                _inventoryItemInfo = slot.Item.Info;
                DescriptionItemText.text = _inventoryItemInfo.Description;
                DropItemButton.gameObject.SetActive(true);


                if (slot.Item.Info.ItemEquippableType == ItemIsEquippableType.Equippable) {
                    EquipItemButton.gameObject.SetActive(true);
                } else {
                    EquipItemButton.gameObject.SetActive(false);
                }

            }


        }
    }
}