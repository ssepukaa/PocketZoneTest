using System.Collections.Generic;
using Assets.Scripts.Enemy.Abstract;
using Assets.Scripts.Infra.Boot;
using Assets.Scripts.Infra.Game.Abstract;
using Assets.Scripts.InventoryObject.Abstract;
using Assets.Scripts.Player;
using Assets.Scripts.Player.Abstract;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Infra.Game.Data {
    [CreateAssetMenu(fileName = "GameResourceData", menuName = "PocketZoneTest/GameResourceData")]
    public class GameResourceData : ScriptableObject, IGameResourceData {
        public Bootstrapper Bootstrapper;
        public IUIController IUIController;
        public IPlayerController _player;
        public GameMode GameMode;
        public GameState GameState;
        public IDamageSystem DamageSystem { get; set; }
        private List<IEnemyController> _enemies = new List<IEnemyController>();
        
        public IPlayerController Player {
            get => _player;
            set => _player = value;
        }

        public GameObject LootPrefab => _lootPrefab;
        public GameObject BulletPrefab => _bulletPrefab;
        public GameObject EnemyPrefab => _enemyPrefab;
        public int NumberOfEnemies => _numberOfEnemies;
        public float SpawnRadius => _spawnRadius;

        public List<IEnemyController> Enemies {
            get => _enemies;
            set => _enemies = value;
        }

        [SerializeField] private GameObject _lootPrefab;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private GameObject _enemyPrefab; // Префаб вашего врага
        [SerializeField] private int _numberOfEnemies = 3; // Количество врагов для спавна
        [SerializeField] private float _spawnRadius = 15f; // Радиус вокруг центра, где могут появляться враги

    }
}
