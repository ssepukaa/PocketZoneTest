using Assets.Scripts.Infra.Game;
using Assets.Scripts.InventoryObject;
using Assets.Scripts.InventoryObject.Abstract;
using Assets.Scripts.InventoryObject.Data;
using Assets.Scripts.Player.Abstract;
using Assets.Scripts.Player.Data;
using Assets.Scripts.UI;
using Assets.Scripts.UI.InventoryUI;
using Assets.Scripts.Weapon;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Player {
    public class PlayerController : MonoBehaviour, IPlayerController {
      

        public PlayerResourceData rd;
        

        public void Construct(IGameController gameController, IUIController uiController) {
            rd.GameController = gameController;
            rd.UIController = uiController;
            rd.PlayerInput = GetComponent<PlayerInputComp>();
            rd.WeaponController = GetComponentInChildren<WeaponController>();
            rd.WeaponController.Construct(this);
            rd.Inventory = new Inventory(rd.CapacityInventory);
            rd.Inventory.OnOneItemInSelectedSlotDroppedEvent += OnInventoryOneItemInSelectedSlotRemoved;
            rd.Inventory.OnRemoveOneAmountItemInSelectedSlotEquippedEvent += InventoryOnOnRemoveOneAmountItemInSelectedSlotEquipped;
            rd.Inventory.OnOneItemAmmoRemovedEvent += OnOneItemAmmoRemovedEvent;
            rd.PlayerInput = GetComponent<PlayerInputComp>();
            rd.PlayerInput.Construct(this);
            rd.UIController.SetInventory(rd.Inventory);
            rd.UIInventory = FindObjectOfType<UIInventory>();
            rd.UIInventory.Construct(this);
            rd.PlayerLootTrigger = GetComponentInChildren<PlayerLootTrigger>();
            rd.PlayerLootTrigger.Construct(this);

        }

        private void InventoryOnOnRemoveOneAmountItemInSelectedSlotEquipped(object sender, IInventoryItemInfo info, int amount) {
            UpdateWeapon(info);
        }

        private void UpdateWeapon(IInventoryItemInfo info) {
            rd.WeaponController.UpdateWeapon(info);
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

        private void OnOneItemAmmoRemovedEvent(object sender, IInventoryItem item, int amount) {
            Debug.Log("Fire Continue!");
            rd.GameController.CreateBullet(this, rd.WeaponController.GetMuzzleTransform(), item.Info, amount);
        }

        public void StartFire() {
            Debug.Log("Fire Start!");
            if ((rd.Inventory.WeaponSlot==null || rd.Inventory.WeaponSlot.IsEmpty)) return;
            
            rd.Inventory.RemoveOneAmountItemInAmmoByType(this, rd.Inventory.WeaponSlot.Item.Info.AmmoType);
        }

        private void OnDestroy() {
            rd.Inventory.OnOneItemInSelectedSlotDroppedEvent -= OnInventoryOneItemInSelectedSlotRemoved;
            rd.Inventory.OnOneItemAmmoRemovedEvent -= OnOneItemAmmoRemovedEvent;
        }
    }
}