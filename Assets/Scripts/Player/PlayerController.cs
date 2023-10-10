using Assets.Scripts.Components;
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
        public IEnemyController TargetEnemy { get; set; }

        public PlayerResourceData RD;
        public PlayerModelData MD;
        public PlayerInputComp PlayerInput { get; set; }
        public UIInventory UIInventory { get; set; }
        public IPlayerLootTrigger LootTrigger { get; set; }

        public WeaponController WeaponController { get; set; }
        public PlayerSenseTrigger SenseTrigger { get; set; }
        public PlayerHealthSystem HealthSystem { get; set; }

        public Vector2 Position => transform.position;

        public Transform TransformPlayer => this.transform;

        public float MaxHealth => RD.MaxBaseHealth;

        public float CurrentHealth {
            get => MD.CurrentHealth;
            set => MD.CurrentHealth = value;
        }
        public IInventory Inventory { get; set; }
        public UIHealthBar UiHealthBar { get; private set; }
        public void Construct(IGameController gameController, IUIController uiController) {
            RD.GameController = gameController;
            RD.UIController = uiController;

            PlayerInput = GetComponent<PlayerInputComp>();
            WeaponController = GetComponentInChildren<WeaponController>();
            WeaponController.Construct(this);
            Inventory = new Inventory(RD.CapacityInventory);
            Inventory.OnOneItemInSelectedSlotDroppedEvent += OnInventoryOneItemInSelectedSlotRemoved;
            Inventory.OnRemoveOneAmountItemInSelectedSlotEquippedEvent += InventoryOnOnRemoveOneAmountItemInSelectedSlotEquipped;
            Inventory.OnOneItemAmmoRemovedEvent += OnOneItemAmmoRemovedEvent;
            PlayerInput = GetComponent<PlayerInputComp>();
            PlayerInput.Construct(this);
            RD.UIController.SetInventory(Inventory);
            UIInventory = FindObjectOfType<UIInventory>();
            UIInventory.Construct(this);
            HealthSystem = new PlayerHealthSystem(this);
            LootTrigger = GetComponentInChildren<PlayerLootTrigger>();
            LootTrigger.Construct(this);
            SenseTrigger = GetComponentInChildren<PlayerSenseTrigger>();
            SenseTrigger.Construct(this);
            UiHealthBar = GetComponentInChildren<UIHealthBar>();
            UiHealthBar.Construct(HealthSystem);
            HealthSystem.Refresh();
            Debug.Log("Construct PlayerController OK!");
        }

        private void InventoryOnOnRemoveOneAmountItemInSelectedSlotEquipped(object sender, IInventoryItemInfo info, int amount) {
            UpdateWeapon(info);
        }

        private void UpdateWeapon(IInventoryItemInfo info) {
            WeaponController.UpdateWeapon(info);
        }

        private void OnInventoryOneItemInSelectedSlotRemoved(object obj, IInventoryItemInfo itemInfo, int amount) {
            Debug.Log($"PlayerController Item Removed!  {itemInfo.ItemType} + Amount: {amount}");
            RD.GameController.CreateLoot(this, transform.position, itemInfo, amount);
        }

        public bool CollectLoot(object sender, IInventoryItem item) {
            Inventory.TryToAdd(sender, item);
            return true;
        }


        public void OpenInventory() {
            RD.UIController.ShowWindow(UIWindowsType.Inventory);
        }

        private void OnOneItemAmmoRemovedEvent(object sender, IInventoryItem item, int amount) {
            Debug.Log("Fire Continue!");
            RD.GameController.CreateBullet(this, WeaponController.GetMuzzleTransform(), item.Info, amount);
        }

        public void StartFire() {
            Debug.Log("Fire Start!");
            if (TargetEnemy == null) {
                Debug.Log("No Target!!");
                return;
            }
            if ((Inventory.WeaponSlot==null || Inventory.WeaponSlot.IsEmpty)) return;
            if (Inventory.WeaponSlot.Item.Info.WeaponInfo == null) return;
            Inventory.RemoveOneAmountItemInAmmoByType(this, Inventory.WeaponSlot.Item.Info.WeaponInfo.AmmoType);
        }

        private void OnDestroy() {
            Inventory.OnOneItemInSelectedSlotDroppedEvent -= OnInventoryOneItemInSelectedSlotRemoved;
            Inventory.OnOneItemAmmoRemovedEvent -= OnOneItemAmmoRemovedEvent;
        }

        public void ApplyDamage(object sender, float damageAmount) {
            HealthSystem.ApplyDamage(damageAmount);
        }

        public void Death() {
            Destroy(gameObject);
        }
    }
}