using System.Collections;
using Assets.Scripts.Main.Boot.Data;
using Assets.Scripts.Main.Game;
using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Main.Boot {
    public class Bootstrapper : MonoBehaviour, IBootstrapper {
        public BootstrapperResData Data;
        bool _isInitGameDone;
        bool _isInitUIDone;

        void Awake() {
            Application.targetFrameRate = 60;
            
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
            Data .GameController = Instantiate(Data.PrefabGameGameobject).GetComponent<GameController>();
            Data.UiController = Instantiate(Data.PrefabUIGamobject).GetComponent<UIController>();
            yield return new WaitForSeconds(1f);
            Debug.Log("Create Game and UI");

        }

        private IEnumerator InitGame() {

            Data.GameController.Construct(this, Data.UiController);
            while (!_isInitGameDone) {
                yield return null;
            }
        }

        private IEnumerator InitUI() {

            Data.UiController.Construct(this, Data.GameController);
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

            Data.GameState = GameStateTypes.Menu;
            Data.GameController.LoadSceneComplete(Data.GameState);
            Data.UiController.LoadSceneComplete(Data.GameState);
            // Вызов события после загрузки сцены
        }
    }
}
