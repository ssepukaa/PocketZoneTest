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
        public IPlayerController PlayerController;
        public GameMode GameMode;
        public GameState GameState;
        public IGameInventoryItemsFactory GameInventoryItemsFactory;
        [SerializeField] private GameObject _lootPrefab;


        public GameObject LootPrefab => _lootPrefab;
        

        public IGameInventoryItemsFactory InventoryItemsFactory {
            get => GameInventoryItemsFactory;
            set => GameInventoryItemsFactory = value;
        }
    }
}