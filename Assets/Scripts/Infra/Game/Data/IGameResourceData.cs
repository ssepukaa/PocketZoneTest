using System.Collections.Generic;
using Assets.Scripts.Enemy.Abstract;
using Assets.Scripts.Infra.Game.Abstract;
using Assets.Scripts.Player.Abstract;
using UnityEngine;

namespace Assets.Scripts.Infra.Game.Data {
    public interface IGameResourceData {
        public GameObject LootPrefab { get; }
        //public IGameInventoryItemsFactory InventoryItemsFactory { get; }
        GameObject BulletPrefab { get; }
        GameObject EnemyPrefab { get; }
        int NumberOfEnemies { get; }
        float SpawnRadius { get; }
        IPlayerController Player { get; }
        
        IDamageSystem DamageSystem { get; set; }
        List<IEnemyController> Enemies { get; set; }
    }
}