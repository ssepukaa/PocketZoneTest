using System.Collections.Generic;
using Assets.Scripts.Enemy.Abstract;
using Assets.Scripts.InventoryObject.Data;
using Assets.Scripts.Main.Boot;
using Assets.Scripts.Main.Game.Abstract;
using Assets.Scripts.Player.Abstract;
using Assets.Scripts.Player.Data;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Main.Game.Data {
    public interface IGameResourceData {
        List<IEnemyController> Enemies { get; set; }
        IGameMode GameMode { get; set; }
        IBootstrapper Bootstrapper { get; set; }
        IUIController UIController { get; set; }
        IGameState GameState { get; set; }
        IPlayerController Player { get; set; }
        IDamageSystem DamageSystem { get; set; }
        PlayerModelData MdPlayer { get; set; }
        ItemsInfoDataBase ItemsDatabase { get; }
        GameObject LootPrefab { get; }
        GameObject BulletPrefab { get; }
        GameObject EnemyPrefab { get; }
        int NumberOfEnemies { get; }
        float SpawnRadius { get; }
    }
}