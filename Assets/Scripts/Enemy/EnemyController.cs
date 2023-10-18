using System.Collections;
using Assets.Scripts.Components;
using Assets.Scripts.Enemy.Abstract;
using Assets.Scripts.Enemy.DataRes;
using Assets.Scripts.InventoryObject;
using Assets.Scripts.InventoryObject.Abstract;
using Assets.Scripts.Main.Game.Abstract;
using Assets.Scripts.Player.Abstract;
using Pathfinding;
using UnityEngine;

namespace Assets.Scripts.Enemy {
    // Контроллер противника
    public class EnemyController : MonoBehaviour, IEnemyController, IDamageable {
        public EnemyResourceData RD;
        public EnemyHealthSystem HealthSystem { get; private set; }
        private EnemySenseTrigger _senseTrigger;
        public UIHealthBar UiHealthBar { get; set; }
        public IInventorySlot WeaponSlot { get; set; }
        public float CurrentHealth { get; set; }
        public float MaxHealth => RD.MaxBaseHealth;
        public Vector2 Position => transform.position;
        public IPlayerController TargetEnemy { get; set; }

        public AIDestinationSetter DestinationSetter { get; set; }

        IGameController _gameController;
        SpriteRenderer _spriteRenderer;
        EnemyBattleSystem _battleSystem;
        

        public void Construct(IGameController gameController) {

            _gameController = gameController;
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>(); 
            //_spriteRenderer.sprite = RD.SpriteEnemy;
            WeaponSlot = new InventorySlot();
            WeaponSlot.Item = new InventoryItem(RD.Info);
            Debug.Log($"WeaponSlot.amount = {WeaponSlot.Item.Amount}");

            HealthSystem = new EnemyHealthSystem(this);

            _battleSystem = GetComponent<EnemyBattleSystem>();
            _battleSystem.Construct(this);
            _senseTrigger = GetComponentInChildren<EnemySenseTrigger>();
            _senseTrigger.Construct(this);
            DestinationSetter = GetComponent<AIDestinationSetter>();
            UiHealthBar = GetComponentInChildren<UIHealthBar>();
            UiHealthBar.Construct(HealthSystem);
            HealthSystem.Refresh();
            Debug.Log("Construct EnemyController OK!");
           
        }

        public void TakeDamage(float damage) {
            HealthSystem.ApplyDamage(damage);
        }

        public void Death() {
            Destroy(gameObject);
            _gameController.CreateLoot(this,Position,RD.LootInfo,1);
        }
    }
}
