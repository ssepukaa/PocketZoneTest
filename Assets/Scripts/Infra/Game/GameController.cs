using Assets.Scripts.Infra.Boot;
using Assets.Scripts.Infra.Game.Data;
using Assets.Scripts.Player;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Infra.Game {
   

    public class GameController : MonoBehaviour, IGameController {
        private Bootstrapper _bootstrapper;
        private IUIController _ui;
        private IPlayerController _player;
        private GameMode _mode;
        private GameState _state;
        [SerializeField] private GameModelData _md;
        [SerializeField] private GameResourceData _rd;

        // Use this for initialization
        void Awake() {
            DontDestroyOnLoad(this);
        }

        public void Construct(Bootstrapper bootstrapper, IUIController uiController) {
            _bootstrapper = bootstrapper;
            _ui = uiController;
            _mode = new GameMode(this);
            _state = new GameState(this);
            _bootstrapper.InitGameComplete();
           
        }

        public void LoadMenuComplete() {
            
        }

        void Update() {

        }
    }
}