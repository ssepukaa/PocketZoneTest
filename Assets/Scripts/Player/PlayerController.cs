using Assets.Scripts.Enemy.Abstract;
using Assets.Scripts.Infra.Game.Abstract;
using Assets.Scripts.InventoryObject;
using Assets.Scripts.InventoryObject.Abstract;
using Assets.Scripts.Player.Abstract;
using Assets.Scripts.Player.Data;
using Assets.Scripts.UI;
using Assets.Scripts.UI.InventoryUI;
using Assets.Scripts.Weapon;
using UnityEngine;

namespace Assets.Scripts.Player {
    public class PlayerController : MonoBehaviour, IPlayerController, IShooter, IController {
      

        public PlayerResourceData RD;
        public PlayerModelData MD;

        public IEnemyController TargetEnemy {
            get => RD.Target;
            set => RD.Target = value;
        }


        public void Construct(IGameController gameController, IUIController uiController) {
            RD.GameController = gameController;
            RD.UIController = uiController;
            
            RD.PlayerInput = GetComponent<PlayerInputComp>();
            RD.WeaponController = GetComponentInChildren<WeaponController>();
            RD.WeaponController.Construct(this);
            RD.Inventory = new Inventory(RD.CapacityInventory);
            RD.Inventory.OnOneItemInSelectedSlotDroppedEvent += OnInventoryOneItemInSelectedSlotRemoved;
            RD.Inventory.OnRemoveOneAmountItemInSelectedSlotEquippedEvent += InventoryOnOnRemoveOneAmountItemInSelectedSlotEquipped;
            RD.Inventory.OnOneItemAmmoRemovedEvent += OnOneItemAmmoRemovedEvent;
            RD.PlayerInput = GetComponent<PlayerInputComp>();
            RD.PlayerInput.Construct(this);
            RD.UIController.SetInventory(RD.Inventory);
            RD.UIInventory = FindObjectOfType<UIInventory>();
            RD.UIInventory.Construct(this);
            RD.PlayerLootTrigger = GetComponentInChildren<PlayerLootTrigger>();
            RD.PlayerLootTrigger.Construct(this);

        }

        private void InventoryOnOnRemoveOneAmountItemInSelectedSlotEquipped(object sender, IInventoryItemInfo info, int amount) {
            UpdateWeapon(info);
        }

        private void UpdateWeapon(IInventoryItemInfo info) {
            RD.WeaponController.UpdateWeapon(info);
        }

        private void OnInventoryOneItemInSelectedSlotRemoved(object obj, IInventoryItemInfo itemInfo, int amount) {
            Debug.Log($"PlayerController Item Removed!  {itemInfo.ItemType} + Amount: {amount}");
            RD.GameController.CreateLoot(this, transform.position, itemInfo, amount);
        }

        public bool CollectLoot(object sender, IInventoryItem item) {
            RD.Inventory.TryToAdd(sender, item);
            return true;
        }


        public void OpenInventory() {
           RD.UIController.ShowWindow(UIWindowsType.Inventory);
        }

        private void OnOneItemAmmoRemovedEvent(object sender, IInventoryItem item, int amount) {
            Debug.Log("Fire Continue!");
            RD.GameController.CreateBullet(this, RD.WeaponController.GetMuzzleTransform(), item.Info, amount);
        }

        public void StartFire() {
            Debug.Log("Fire Start!");
            if ((RD.Inventory.WeaponSlot==null || RD.Inventory.WeaponSlot.IsEmpty)) return;
            if(RD.Inventory.WeaponSlot.Item.Info.WeaponInfo == null) return;
            RD.Inventory.RemoveOneAmountItemInAmmoByType(this, RD.Inventory.WeaponSlot.Item.Info.WeaponInfo.AmmoType);
        }

        private void OnDestroy() {
            RD.Inventory.OnOneItemInSelectedSlotDroppedEvent -= OnInventoryOneItemInSelectedSlotRemoved;
            RD.Inventory.OnOneItemAmmoRemovedEvent -= OnOneItemAmmoRemovedEvent;
        }

        public Vector2 Position => transform.position;
        public void TargetPosition(Vector2 position, bool isTarget) {
            throw new System.NotImplementedException();
        }
    }
}