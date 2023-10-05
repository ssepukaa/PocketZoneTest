using Assets.Scripts.Infra.Boot;
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
        public IGameInventoryItemsFactory GameInventoryItemsFactory;
        [SerializeField] private GameObject _lootPrefab;
        [SerializeField] private GameObject _bulletPrefab;


        public GameObject LootPrefab => _lootPrefab;


        public IGameInventoryItemsFactory InventoryItemsFactory {
            get => GameInventoryItemsFactory;
            set => GameInventoryItemsFactory = value;
        }

        public GameObject BulletPrefab => _bulletPrefab;


        public IPlayerController Player {
            get => _player;
            set => _player = value;
        }
    }
}
