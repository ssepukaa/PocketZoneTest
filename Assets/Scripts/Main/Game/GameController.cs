using System;
using System.Collections;
using Assets.Scripts.InventoryObject;
using Assets.Scripts.InventoryObject.Abstract;
using Assets.Scripts.Main.Boot;
using Assets.Scripts.Main.Game.Abstract;
using Assets.Scripts.Main.Game.Data;
using Assets.Scripts.Main.Game.SaveLoad;
using Assets.Scripts.Player;
using Assets.Scripts.Player.Data;
using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Main.Game {
    // Игра
    public enum SceneNames { Boot, Menu, Game1, Game2, }

    public class GameController : MonoBehaviour, IGameController {
        public IGameResourceData RD => _rd;

        public IGameMode GameMode {
            get => RD.GameMode;
            set => RD.GameMode = value;
        }

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
            //RD.DamageSystem = new DamageSystem(this);

            RD.Bootstrapper.InitGameComplete();

        }

        public IEnumerator LoadSceneCoroutine(SceneNames sceneName, GameStateTypes gameState) {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName.ToString());
            while (!asyncOperation.isDone) {
                // Здесь можно добавить индикатор загрузки, если нужно
                yield return null;
            }

            // Вызов события после загрузки сцены
            LoadSceneComplete(gameState);
            RD.UIController.LoadSceneComplete(gameState);

        }

        public void LoadSceneComplete(GameStateTypes gameState) {
            RD.GameState.SetState(gameState);
            SceneNames sceneName = (SceneNames)Enum.Parse(typeof(SceneNames), SceneManager.GetActiveScene().name);
            switch (sceneName) {
                case SceneNames.Boot:
                    break;
                case SceneNames.Menu:
                    RD.Player = null;
                    RD.MdPlayer = null;
                    LoadPlayerData();

                    break;
                case SceneNames.Game1:
                    ConstructPlayer();

                    ConstructEnemies();
                    RD.GameMode.ChangeState(sceneName);
                    break;
                case SceneNames.Game2:
                    ConstructPlayer();

                    ConstructEnemies();
                    RD.GameMode.ChangeState(sceneName);
                    break;
                default:
                    break;
            }
        }

        public void LoadPlayerData() {

            RD.MdPlayer = SaveLoadManager.LoadPlayer(RD.ItemsDatabase);

            if (RD.MdPlayer == null) {
                Debug.Log("Load Player Data error, new PlayerData , new Inventory");
                // Если данных нет, инициализируйте новый экземпляр PlayerModelData
                RD.MdPlayer = new PlayerModelData();
                
                RD.MdPlayer._inventory = new Inventory(RD.MdPlayer.BaseInventoryCapacity, RD.MdPlayer);
                //RD.MdPlayer._clipSlot = new InventorySlot();
            }
            else {
                Debug.Log($"Load Player Data OK. Inventory!=null:" +
                          $" {RD.MdPlayer._inventory!=null}. Loaded data succes!!!");
            }
            
           
        }

        public void SaveDataPlayer() {
            SaveLoadManager.SavePlayer(RD.MdPlayer, RD.ItemsDatabase);
        }

        public bool GetIsFirstLevelComplete() {
            if(RD.MdPlayer == null) return false;
            if(RD.MdPlayer.IsCompleteGame1) return true;
            return false;
        }

        private void ConstructPlayer() {
            
            RD.Player = FindObjectOfType<PlayerController>();
            RD.Player.Construct(this, RD.UIController, RD.MdPlayer);
        }

        private void ConstructEnemies() {
            var spawner = new EnemySpawner(this);
            

        }

        public void CreateLoot(object sender, Vector2 position, IInventoryItemInfo info, int amount) {
            var factory = new GameItemsFactory(this);
            factory.CreateInventoryLootItem(sender, position, info, amount);
        }

        public void CreateBullet(object sender, Transform transform, IInventoryItemInfo info, int amount) {
            var factory = new GameItemsFactory(this);
            factory.CreateBullet(sender, transform, info, amount);
        }

        void Update() { }

        #region MenuScene

        public void PlayButtonInSceneMenu(SceneNames scene) {
            StartCoroutine(LoadSceneCoroutine(scene, GameStateTypes.Game));
        }

        #endregion

        public void TaskCompleted() {
            // Добавить вызов окна победы 
            RD.UIController.ShowPopup(UIPopupType.TaskComplete);
        }

        public void StartLoadSceneCoroutine(SceneNames nameScene, GameStateTypes stateType) {
            StartCoroutine(LoadSceneCoroutine(nameScene, stateType));
        }
    }
}