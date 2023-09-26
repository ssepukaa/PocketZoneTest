using System.Collections;
using Assets.Scripts.Infra.Game;
using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Infra.Boot {
    public class Bootstrapper : MonoBehaviour, IBootstrapper {
       private IGameController _gameController;
       private IUIController _uiController;
       private GameStateTypes _gameState;
       [SerializeField]private GameObject prefGame;
       [SerializeField]private GameObject prefUI;

       
       private bool _isInitGameDone;
       private bool _isInitUIDone;

       void Awake() {
           DontDestroyOnLoad(this);
       }

       void Start() {
           StartCoroutine(Initializing());

       }
        private IEnumerator Initializing() {
            
            Debug.Log("Loading...");

            yield return StartCoroutine(CreateComponent());
            yield return StartCoroutine(InitGame());
            yield return StartCoroutine(InitUI());

            yield return new WaitForSeconds(0.5f);
            Debug.Log("Init OK!");
            LoadMenu();
        }

        private IEnumerator CreateComponent() {
            _gameController = Instantiate(prefGame).GetComponent<GameController>();
            _uiController = Instantiate(prefUI).GetComponent<UIController>();
            yield return new WaitForSeconds(1f);
            Debug.Log("Create Game and UI");

        }

        private IEnumerator InitGame() {
            
            _gameController.Construct(this, _uiController);
            while (!_isInitGameDone) {
                yield return null;
            }
        }

        private IEnumerator InitUI() {
           
            _uiController.Construct(this, _gameController);
            while (!_isInitUIDone) {
                yield return null;
            }
        }
        public void InitGameComplete() {
            _isInitGameDone = true;
            Debug.Log("Init Game OK");
        }

        public void InitUIComplete() {
            _isInitUIDone = true;
            Debug.Log("Init UI OK");
        }

        private void LoadMenu() {
            StartCoroutine(LoadSceneCoroutine("Menu"));

        }
        private IEnumerator LoadSceneCoroutine(string sceneName) {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            while (!asyncOperation.isDone) {
                // Здесь можно добавить индикатор загрузки, если нужно
                yield return null;
            }

            _gameState = GameStateTypes.Menu;
            _gameController.LoadSceneComplete(_gameState);
            _uiController.LoadSceneComplete(_gameState);
            // Вызов события после загрузки сцены
        }
    }

    public interface IBootstrapper { }
}
