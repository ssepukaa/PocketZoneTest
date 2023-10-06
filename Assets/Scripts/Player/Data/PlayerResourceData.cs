using Assets.Scripts.Enemy.Abstract;
using Assets.Scripts.Infra.Game.Abstract;
using Assets.Scripts.InventoryObject.Abstract;
using Assets.Scripts.Player.Abstract;
using Assets.Scripts.UI;
using Assets.Scripts.UI.InventoryUI;
using Assets.Scripts.Weapon;
using UnityEngine;

namespace Assets.Scripts.Player.Data {
    [CreateAssetMenu(fileName = "PlayerResourceData", menuName = "PocketZoneTest/PlayerResourceData")]
    public class PlayerResourceData : ScriptableObject {
        public IGameController GameController;
        public IUIController UIController;
        public IInventory Inventory;
        //public ItemsInfoDataBase inventoryInfosData;
        public PlayerInputComp PlayerInput;
        public UIInventory UIInventory;
        public int CapacityInventory = 16;
        public IPlayerLootTrigger PlayerLootTrigger;

        public WeaponController WeaponController;
        private IEnemyController _target;


        public IEnemyController Target {
            get => _target;
            set => _target = value;
        }
    }
}
