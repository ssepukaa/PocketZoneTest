﻿using Assets.Scripts.Infra.Game.Abstract;
using Assets.Scripts.InventoryObject.Abstract;
using Assets.Scripts.Player.Abstract;
using Pathfinding;
using UnityEngine;

namespace Assets.Scripts.Enemy.Abstract {
    public interface IEnemyController {
        IPlayerController TargetEnemy { get; set; }
        IInventorySlot WeaponSlot { get;}
        AIDestinationSetter DestinationSetter { get; set; }
        Vector2 Position { get; }
        float CurrentHealth { get; set; }
        float MaxHealth { get; }
        void Construct(IGameController  gameController);
        void Death();
    }
}
