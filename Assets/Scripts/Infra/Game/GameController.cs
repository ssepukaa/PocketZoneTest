using System.Collections;
using Assets.Scripts.Infra.Boot;
using Assets.Scripts.Infra.Game.Data;
using Assets.Scripts.Player;
using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Infra.Game {


    public class GameController : MonoBehaviour, IGameController {
        
        [SerializeField] private GameModelData _md;
        [SerializeField] private GameResourceData _rd;


        void Awake() {
            DontDestroyOnLoad(this);
        }

        public void Construct(Bootstrapper bootstrapper, IUIController uiController) {
            _rd._bootstrapper = bootstrapper;
            _rd._ui = uiController;
            _rd._mode = new GameMode(this);
            _rd._state = new GameState(this);
            _rd._bootstrapper.InitGameComplete();

        }
        private IEnumerator LoadSceneCoroutine(string sceneName, GameStateTypes gameState) {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            while (!asyncOperation.isDone) {
                // Здесь можно добавить индикатор загрузки, если нужно
                yield return null;
            }
            // Вызов события после загрузки сцены
            LoadSceneComplete(gameState);
            _rd._ui.LoadSceneComplete(gameState);

        }
        public void LoadSceneComplete(GameStateTypes gameState) {
            _rd._state.SetState(gameState);
            
            
        }



        void Update() {

        }

      //  #region SceneMenu
        public void PlayButtonInSceneMenu() {
            StartCoroutine(LoadSceneCoroutine("Game1", GameStateTypes.Game));
        }


       // #endregion
    }
}