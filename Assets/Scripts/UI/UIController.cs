using System.Collections;
using Assets.Scripts.Infra.Boot;
using Assets.Scripts.Infra.Game;
using UnityEngine;

namespace Assets.Scripts.UI {
    public class UIController : MonoBehaviour, IUIController {
        private Bootstrapper _bootstrapper;
        private IGameController _gameController;

        // Use this for initialization
        void Start() {
            void Awake() {
                DontDestroyOnLoad(this);
            }
        }

        // Update is called once per frame
        void Update() {

        }

        public void Construct(Bootstrapper bootstrapper, IGameController gameController) {
            _bootstrapper = bootstrapper;
            _gameController = gameController;
            _bootstrapper.InitUIComplete();
        }

        public void LoadMenuComplete() {
            
        }
    }
}