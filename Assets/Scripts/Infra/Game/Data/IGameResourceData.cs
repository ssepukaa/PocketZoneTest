using System.Collections.Generic;
using Assets.Scripts.Enemy.Abstract;
using Assets.Scripts.Infra.Boot;
using Assets.Scripts.Infra.Game.Abstract;
using Assets.Scripts.Player.Abstract;
using Assets.Scripts.Player.Data;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Infra.Game.Data {
    public interface IGameResourceData {
        List<IEnemyController> Enemies { get; set; }
        IGameMode GameMode { get; set; }
        IBootstrapper Bootstrapper { get; set; }
        IUIController UIController { get; set; }
        IGameState GameState { get; set; }
        IPlayerController Player { get; set; }
        IDamageSystem DamageSystem { get; set; }
        PlayerModelData MdPlayer { get; set; }
        GameObject LootPrefab { get; }
        GameObject BulletPrefab { get; }
        GameObject EnemyPrefab { get; }
        int NumberOfEnemies { get; }
        float SpawnRadius { get; }
    }
}