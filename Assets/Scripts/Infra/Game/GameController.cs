using System;
using System.Collections;
using Assets.Scripts.Infra.Boot;
using Assets.Scripts.Infra.Game.Data;
using Assets.Scripts.Player;
using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Infra.Game {
    public enum SceneNames { Boot, Menu, Game1, Game2, }

    public class GameController : MonoBehaviour, IGameController {
        public IGameResourceData RD { get; }
        [SerializeField] private GameModelData _md;
        [SerializeField] private GameResourceData _rd;


        void Awake() {
            DontDestroyOnLoad(this);
        }

        public void Construct(Bootstrapper bootstrapper, IUIController uiController) {
            _rd.Bootstrapper = bootstrapper;
            _rd.IUIController = uiController;
            _rd.GameMode = new GameMode(this);
            _rd.GameState = new GameState(this);
            _rd.GameInventoryItemsFactory = new GameInventoryItemsFactory(this);
            _rd.Bootstrapper.InitGameComplete();

        }

        private IEnumerator LoadSceneCoroutine(string sceneName, GameStateTypes gameState) {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            while (!asyncOperation.isDone) {
                // Здесь можно добавить индикатор загрузки, если нужно
                yield return null;
            }
            // Вызов события после загрузки сцены
            LoadSceneComplete(gameState);
            _rd.IUIController.LoadSceneComplete(gameState);

        }

        public void LoadSceneComplete(GameStateTypes gameState) {
            _rd.GameState.SetState(gameState);
            SceneNames sceneNameEnum = (SceneNames)Enum.Parse(typeof(SceneNames), SceneManager.GetActiveScene().name);
            switch (sceneNameEnum) {
                case SceneNames.Boot:
                    break;
                case SceneNames.Menu:
                    break;
                case SceneNames.Game1:
                    _rd.PlayerController = FindObjectOfType<PlayerController>();
                    _rd.PlayerController.Construct(this, _rd.IUIController);
                    break;
                case SceneNames.Game2:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }


        }

        void Update() {

        }

        #region MenuScene

        public void PlayButtonInSceneMenu() {
            StartCoroutine(LoadSceneCoroutine(SceneNames.Game1.ToString(), GameStateTypes.Game));
        }

        #endregion
    }
}