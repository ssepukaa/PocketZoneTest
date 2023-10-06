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


        //[SerializeField] private GameItemsFactory _gameItemsFactory;
        [SerializeField] private GameObject _lootPrefab;
        [SerializeField] private GameObject _bulletPrefab;


        public GameObject LootPrefab => _lootPrefab;
        public GameObject BulletPrefab => _bulletPrefab;
       // public IGameInventoryItemsFactory InventoryItemsFactory => _gameItemsFactory;
        
        public IPlayerController Player {
            get => _player;
            set => _player = value;
        }
        //  public IDamageSystem DamageSystem  => DamageSystem;
            
    }
}
