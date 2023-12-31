﻿using System.Collections.Generic;
using Assets.Scripts.Enemy.Abstract;
using Assets.Scripts.InventoryObject.Data;
using Assets.Scripts.Main.Boot;
using Assets.Scripts.Main.Game.Abstract;
using Assets.Scripts.Player.Abstract;
using Assets.Scripts.Player.Data;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Main.Game.Data {
    // Хранение ссылок и ресурсов игры - реализация
    [CreateAssetMenu(fileName = "GameResourceData", menuName = "PocketZoneTest/GameResourceData")]
    public class GameResourceData : ScriptableObject, IGameResourceData {
        public List<IEnemyController> Enemies { get => _enemies; set => _enemies = value; }
       // public IDamageSystem DamageSystem { get; set; }
        public IGameMode GameMode { get => _gameMode; set => _gameMode = value; }
        public IBootstrapper Bootstrapper { get => _bootstrapper; set => _bootstrapper = value; }
        public IUIController UIController { get => _uiController; set => _uiController = value; }
        public IGameState GameState { get => _gameState; set => _gameState = value; }
        public IPlayerController Player { get => _player; set => _player = value; }
        public IUIController _uiController;
        public IPlayerController _player;
        public IBootstrapper _bootstrapper;
        public IGameState _gameState;
        public GameObject LootPrefab => _lootPrefab;
        public GameObject BulletPrefab => _bulletPrefab;
        public GameObject EnemyPrefab => _enemyPrefab;
        public int NumberOfEnemies => _numberOfEnemies;
        public float SpawnRadius => _spawnRadius;
        List<IEnemyController> _enemies = new List<IEnemyController>();
        IGameMode _gameMode;
        public PlayerModelData MdPlayer { get => _mdPlayer; set => _mdPlayer = value; }

        public ItemsInfoDataBase ItemsDatabase => _itemsDatabase;

        [SerializeField] PlayerModelData _mdPlayer;

        [SerializeField] ItemsInfoDataBase _itemsDatabase; /* получите базу данных предметов */
        [SerializeField] GameObject _lootPrefab;
        [SerializeField] GameObject _bulletPrefab;
        [SerializeField] GameObject _enemyPrefab; // Префаб вашего врага
        [SerializeField] int _numberOfEnemies = 3; // Количество врагов для спавна
        [SerializeField] float _spawnRadius = 15f; // Радиус вокруг центра, где могут появляться враги

    }
}
