using System;
using System.Collections;
using Assets.Scripts.Infra.Boot;
using Assets.Scripts.Infra.Game;
using Assets.Scripts.UI.Data;
using UnityEngine;

namespace Assets.Scripts.UI {
    public enum UIWindowsType { StartMenu, }
    public class UIController : MonoBehaviour, IUIController {
        
        private BaseWindowsUI[] _uiWindows;
        [SerializeField] private UIModelData _md;
        [SerializeField] private UIResourceData _rd;



        void Awake() {
            DontDestroyOnLoad(this);
        }



        public void ShowWindow(UIWindowsType windowType) {
            foreach (var window in _uiWindows) {
                window.gameObject.SetActive(false);
            }
            foreach (var window in _uiWindows) {
                if (window.idUiWindowsType == windowType) {
                    window.gameObject.SetActive(true);
                }

            }
        }

        public void Construct(Bootstrapper bootstrapper, IGameController gameController) {
            _rd._bootstrapper = bootstrapper;
            _rd._gameController = gameController;
            _rd._bootstrapper.InitUIComplete();
        }

        public void LoadSceneComplete(GameStateTypes gameState) {
            _uiWindows = FindObjectsOfType<BaseWindowsUI>();
            foreach (var item in _uiWindows) {
                item.Construct(this);
            }

            switch (gameState) {
                case GameStateTypes.Loading:
                    Debug.Log("Нет варианта для Loading");
                    break;
                case GameStateTypes.Menu:
                    ShowWindow(UIWindowsType.StartMenu);
                    break;
                case GameStateTypes.Game:
                    break;
                case GameStateTypes.Pause:
                    break;
                default:
                    Debug.Log("Нет варианта для Default");
                    break;
            }

        }

       // #region SceneMenu

        public void PlayButtonInSceneMenu() {
            _rd._gameController.PlayButtonInSceneMenu();
        }

       // #endregion

    }
}