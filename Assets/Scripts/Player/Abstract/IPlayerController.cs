using System;
using Assets.Scripts.Enemy.Abstract;
using Assets.Scripts.Infra.Game.Abstract;
using Assets.Scripts.InventoryObject.Abstract;
using Assets.Scripts.InventoryObject.Data;
using Assets.Scripts.Player.Data;
using Assets.Scripts.UI;
using Assets.Scripts.Weapon;
using UnityEngine;

namespace Assets.Scripts.Player.Abstract {
    public interface IPlayerController {
        event Action<int, int> OnMissionChangedEvent;
        IGameController GameController { get; }
        IInventory Inventory { get; set; }
        IWeaponController Weapon { get; }
        IEnemyController TargetEnemy { get; set; }
        Vector2 Position { get; }
        Transform TransformPlayer { get; }
        bool TryLootCollect(object sender, IInventoryItem item);
        float CurrentHealth { get; set; }
        float MaxHealth { get; }
        void Construct(IGameController gameController, IUIController uiController, PlayerModelData modelData);
        void StartFire();
        void ApplyDamage(object sender, float damageAmount);
        void Death();
        void UpdateTaskUI();
        void UpdateTask(int collectedCoins, int coinCountTarget, InventoryItemType itemType);
        void Reload();
        void NoAmmo();
        void AfterMissionCompleteButton();
        void AfterDeathButton();
    }
}