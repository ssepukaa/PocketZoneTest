using Assets.Scripts.Infra.Game.Abstract;
using Assets.Scripts.InventoryObject.Abstract;
using Assets.Scripts.Player.Abstract;
using Pathfinding;
using UnityEngine;

namespace Assets.Scripts.Enemy.Abstract {
    public interface IEnemyController {
        public void Construct(IGameController  gameController);

        Vector2 Position { get; }
        IPlayerController TargetEnemy { get; set; }
        public AIDestinationSetter DestinationSetter { get; set; }
        IInventorySlot WeaponSlot { get;}
        float CurrentHealth { get; set; }
        float MaxHealth { get; }
        void Death();
    }
}
