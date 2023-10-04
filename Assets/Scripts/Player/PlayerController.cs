﻿using Assets.Scripts.Infra.Game;
using Assets.Scripts.InventoryObject;
using Assets.Scripts.InventoryObject.Abstract;
using Assets.Scripts.InventoryObject.Data;
using Assets.Scripts.Player.Abstract;
using Assets.Scripts.Player.Data;
using Assets.Scripts.UI;
using Assets.Scripts.UI.InventoryUI;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Player {
    public class PlayerController : MonoBehaviour, IPlayerController {
      

        public PlayerResourceData rd;
        

        public void Construct(IGameController gameController, IUIController uiController) {
            rd.GameController = gameController;
            rd.UIController = uiController;
            rd.PlayerInput = GetComponent<PlayerInputComp>();
            rd.Inventory = new Inventory(rd.CapacityInventory);
            rd.Inventory.OnInventoryOneItemInSelectedSlotRemovedEvent += OnInventoryOneItemInSelectedSlotRemoved;
            rd.PlayerInput = GetComponent<PlayerInputComp>();
            rd.PlayerInput.Construct(this);
            rd.UIController.SetInventory(rd.Inventory);
            rd.UIInventory = FindObjectOfType<UIInventory>();
            rd.UIInventory.Construct(this);
            rd.PlayerLootTrigger = GetComponentInChildren<PlayerLootTrigger>();
            rd.PlayerLootTrigger.Construct(this);

        }

        private void OnInventoryOneItemInSelectedSlotRemoved(object obj, IInventoryItemInfo itemInfo, int amount) {
            Debug.Log($"PlayerController Item Removed!  {itemInfo.ItemType} + Amount: {amount}");
            rd.GameController.CreateLoot(this, transform.position, itemInfo, amount);
        }

        public bool CollectLoot(object sender, IInventoryItem item) {
            rd.Inventory.TryToAdd(sender, item);
            return true;
        }


        public void OpenInventory() {
           rd.UIController.ShowWindow(UIWindowsType.Inventory);
        }

        private void OnDestroy() {
            rd.Inventory.OnInventoryOneItemInSelectedSlotRemovedEvent -= OnInventoryOneItemInSelectedSlotRemoved;
        }
    }
}