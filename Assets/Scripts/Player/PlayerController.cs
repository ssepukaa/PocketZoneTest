using System;
using Assets.Scripts.Components;
using Assets.Scripts.Enemy.Abstract;
using Assets.Scripts.InventoryObject.Abstract;
using Assets.Scripts.InventoryObject.Data;
using Assets.Scripts.Main.Game;
using Assets.Scripts.Main.Game.Abstract;
using Assets.Scripts.Player.Abstract;
using Assets.Scripts.Player.Data;
using Assets.Scripts.UI;
using Assets.Scripts.UI.InventoryUI;
using Assets.Scripts.Weapon;
using UnityEngine;

namespace Assets.Scripts.Player {
    // Реализация игрока
    public class PlayerController : MonoBehaviour, IPlayerController, IShooter {
        public event Action<int, int> OnMissionChangedEvent;

        public IEnemyController TargetEnemy { get; set; }

        public IPlayerLootTrigger LootTrigger { get; set; }

        public IInventory Inventory { get => MD._inventory; set => MD._inventory = value; }

        public IGameController GameController => RD.GameController;

        public IWeaponController Weapon => WeaponController;

        public PlayerInputComp PlayerInput { get; set; }

        public UIInventory UIInventory { get; set; }

        public WeaponController WeaponController { get; set; }

        public PlayerSenseTrigger SenseTrigger { get; set; }

        public PlayerHealthSystem HealthSystem { get; set; }

        public UIHealthBar UiHealthBar { get; private set; }

        public Vector2 Position => transform.position;

        public Transform TransformPlayer => this.transform;

        public float MaxHealth => RD.MaxBaseHealth;

        public float CurrentHealth { get => _currentHealth; set => _currentHealth = value; }

        public PlayerResourceData RD;

        public PlayerModelData MD;

        [SerializeField] float _currentHealth; int _collectedCoins; int _countTargetCoins; InventoryItemType _itemTargetType;

        public void Construct(IGameController gameController, IUIController uiController, PlayerModelData md) {

            RD.GameController = gameController;
            RD.UIController = uiController;
            MD = md;

            PlayerInput = GetComponent<PlayerInputComp>();
            Debug.Log($"Inventory == null : {Inventory == null}");
            Inventory.OnOneItemInSelectedSlotDroppedEvent += OnInventoryOneItemInSelectedSlotRemoved;
            Inventory.OnRemoveOneAmountItemInSelectedSlotEquippedEvent +=   InventoryOnOnRemoveOneAmountItemInSelectedSlotEquipped;
            PlayerInput = GetComponent<PlayerInputComp>();
            PlayerInput.Construct(this);
            
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
            WeaponController = GetComponentInChildren<WeaponController>();
            WeaponController.Construct(this);
            
            // GameController.GameMode.InitTaskPlayer(this);
            Debug.Log("Construct PlayerController OK!");
            
        }


        private void InventoryOnOnRemoveOneAmountItemInSelectedSlotEquipped(object sender, IInventoryItemInfo info, int amount) {
            UpdateWeapon(info);
        }

        private void UpdateWeapon(IInventoryItemInfo info) {
            WeaponController.UpdateWeapon();
        }

        private void OnInventoryOneItemInSelectedSlotRemoved(object obj, IInventoryItemInfo itemInfo, int amount) {
            Debug.Log($"PlayerController Item Removed!  {itemInfo.ItemType} + Amount: {amount}");
            RD.GameController.CreateLoot(this, transform.position, itemInfo, amount);
        }

        public bool TryLootCollect(object sender, IInventoryItem item) {
            var isTakenLoot = Inventory.TryToAdd(sender, item);
            if (item.ItemType == _itemTargetType && isTakenLoot) {
                GameController.GameMode.CollectCoin(this);
                Debug.Log("Collect coin!");
                
            }
            return isTakenLoot;
        }

        public void UpdateTask(int collectedCoins, int coinCountTarget, InventoryItemType itemType) {
            _collectedCoins = collectedCoins;
            _countTargetCoins = coinCountTarget;
            _itemTargetType = itemType;
            UpdateTaskUI();

        }

        public void Reload() {
            
            RD.UIController.ShowPopup(UIPopupType.Reloading);
        }

        public void NoAmmo() {
            RD.UIController.ShowPopup(UIPopupType.NoAmmo);
        }

        public void AfterMissionCompleteButton() {
            MD.IsCompleteGame1 = true;
            SavePlayerData();
            RD.GameController.StartLoadSceneCoroutine(SceneNames.Menu, GameStateTypes.Menu);
        }

        public void AfterDeathButton() {
            RD.GameController.StartLoadSceneCoroutine(SceneNames.Menu, GameStateTypes.Menu );
        }

        public bool GetIsFirstLevelComplete() {
            return MD.IsCompleteGame1;
        }

        public void UpdateTaskUI() {
           
            OnMissionChangedEvent?.Invoke(_collectedCoins, _countTargetCoins);
            WeaponController.UpdateWeapon();

        }
        public void OpenInventory() {
            RD.UIController.ShowWindow(UIWindowsType.Inventory);
        }

        public void StartFire() {

            WeaponController.StartFire();
        }

        private void OnDestroy() {
            Inventory.OnOneItemInSelectedSlotDroppedEvent -= OnInventoryOneItemInSelectedSlotRemoved;

        }
        public void ApplyDamage(object sender, float damageAmount) {
            HealthSystem.ApplyDamage(damageAmount);
        }
        public void Death() {
            Debug.Log("Player Dead!!!!!!!!!!!!!");
            RD.UIController.ShowPopup(UIPopupType.PlayerDead);
            //Destroy(gameObject);
        }

        public void SavePlayerData() {
            RD.GameController.SaveDataPlayer();
        }
    }
}