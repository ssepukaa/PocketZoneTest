using System;
using System.Collections;
using Assets.Scripts.Infra.Boot;
using Assets.Scripts.Infra.Game.Abstract;
using Assets.Scripts.Infra.Game.Data;
using Assets.Scripts.InventoryObject;
using Assets.Scripts.InventoryObject.Abstract;
using Assets.Scripts.Player;
using Assets.Scripts.Player.Data;
using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Infra.Game {
    public enum SceneNames { Boot, Menu, Game1, Game2, }

    public class GameController : MonoBehaviour, IGameController {
        public IGameResourceData RD => _rd;
        public IGameMode GameMode { get => RD.GameMode; set => RD.GameMode = value; }
        public Transform ControllerTransform => transform;


        [SerializeField] GameResourceData _rd;
       


        void Awake() {
            DontDestroyOnLoad(this);
        }

        public void Construct(Bootstrapper bootstrapper, IUIController uiController) {
            RD.Bootstrapper = bootstrapper;
            RD.UIController = uiController;
            RD.GameMode = new GameMode(this);
            RD.GameState = new GameState(this);
            RD.DamageSystem = new DamageSystem(this);

            RD.Bootstrapper.InitGameComplete();

        }

        private IEnumerator LoadSceneCoroutine(string sceneName, GameStateTypes gameState) {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            while (!asyncOperation.isDone) {
                // Здесь можно добавить индикатор загрузки, если нужно
                yield return null;
            }
            // Вызов события после загрузки сцены
            LoadSceneComplete(gameState);
            _rd.UIController.LoadSceneComplete(gameState);

        }

        public void LoadSceneComplete(GameStateTypes gameState) {
            _rd.GameState.SetState(gameState);
            SceneNames sceneName = (SceneNames)Enum.Parse(typeof(SceneNames), SceneManager.GetActiveScene().name);
            switch (sceneName) {
                case SceneNames.Boot:
                    break;
                case SceneNames.Menu:
                    LoadPlayerData();
                    break;
                case SceneNames.Game1:
                    ConstructPlayer();

                    ConstructEnemies();
                    _rd.GameMode.ChangeState(sceneName);
                    break;
                case SceneNames.Game2:
                    ConstructPlayer();

                    ConstructEnemies();
                    _rd.GameMode.ChangeState(sceneName);
                    break;
                default:
                    break;
            }
        }

        public void LoadPlayerData() {
            _rd.MdPlayer = BinarySerializationHelper.DeserializeFromFile<PlayerModelData>();
            if (_rd.MdPlayer == null) {
                // Если данных нет, инициализируйте новый экземпляр PlayerModelData
                _rd.MdPlayer = new PlayerModelData();
                _rd.MdPlayer._inventory = new Inventory(_rd.MdPlayer.BaseInventoryCapacity, _rd.MdPlayer);
                _rd.MdPlayer._clipSlot = new InventorySlot();
            }

        }

        private void ConstructPlayer() {
            RD.Player = FindObjectOfType<PlayerController>();
            RD.Player.Construct(this, RD.UIController, _rd.MdPlayer);
        }

        private void ConstructEnemies() {
            new EnemySpawner(this);
            foreach (var enemy in _rd.Enemies) {
                enemy.Construct(this);
            }
        }

        public void CreateLoot(object sender, Vector2 position, IInventoryItemInfo info, int amount) {
            var factory = new GameItemsFactory(this);
            factory.CreateInventoryLootItem(sender, position, info, amount);
        }

        public void CreateBullet(object sender, Transform transform, IInventoryItemInfo info, int amount) {
            var factory = new GameItemsFactory(this);
            factory.CreateBullet(sender, transform, info, amount);
        }

        void Update() {

        }

        #region MenuScene

        public void PlayButtonInSceneMenu() {
            StartCoroutine(LoadSceneCoroutine(SceneNames.Game1.ToString(), GameStateTypes.Game));
        }

        #endregion

        public void TaskCompleted() {
            // Добавить вызов окна победы 
        }
    }
}