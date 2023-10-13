using Assets.Scripts.Infra.Game.Abstract;
using Assets.Scripts.Infra.Game;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Infra.Boot.Data {
    [CreateAssetMenu(fileName = "BootstrapperResourceData", menuName = "PocketZoneTest/BootstrapperResourceData")]

    public class BootstrapperResData: ScriptableObject {
       
        [SerializeField] IGameController _gameController;
        [SerializeField] IUIController _uiController;
        [SerializeField] GameStateTypes _gameState;
        [SerializeField] GameObject prefGame;
        [SerializeField] GameObject prefUI;

        public IGameController GameController {
            get => _gameController;
            set => _gameController = value;
        }

        public IUIController UiController {
            get => _uiController;
            set => _uiController = value;
        }

        public GameStateTypes GameState {
            get => _gameState;
            set => _gameState = value;
        }

        public GameObject PrefabGameGameobject => prefGame;

        public GameObject PrefabUIGamobject => prefUI;
    }
}
