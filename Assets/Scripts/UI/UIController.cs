using Assets.Scripts.InventoryObject.Abstract;
using Assets.Scripts.Player.Abstract;
using Assets.Scripts.UI.Base;
using Assets.Scripts.UI.Data;
using Assets.Scripts.Weapon;
using System.Collections.Generic;
using Assets.Scripts.Main.Boot;
using Assets.Scripts.Main.Game;
using Assets.Scripts.Main.Game.Abstract;
using UnityEngine;

namespace Assets.Scripts.UI {
    public enum UIWindowsType {
        StartMenu, Inventory, HUD,
    }
    public enum UIPopupType { None, TaskComplete, NoAmmo, Reloading, PlayerDead}
    public class UIController : MonoBehaviour, IUIController {
        public UIResData Data;
        public IWeaponController Weapon => GameController.RD.Player.Weapon;
        public IPlayerController Player => GameController.RD.Player;

        public IBootstrapper Bootstrapper { get => Data.Bootstrapper; set => Data.Bootstrapper = value; }

        public IGameController GameController { get => Data.GameController; set => Data.GameController = value; }
        Queue<UIBasePopups> _popupQueue ;

        [SerializeField] UIBaseWindows[] _uiWindows;
        [SerializeField] UIBasePopups[] _uiPopups;


        void Awake() {
            DontDestroyOnLoad(this);
        }

        public void Construct(IBootstrapper bootstrapper, IGameController gameController) {
           
            Bootstrapper = bootstrapper;
            GameController = gameController;
            Bootstrapper.InitUIComplete();
        }

        public bool GetIsFirstLevelComplete() {
            
            return GameController.GetIsFirstLevelComplete();;
        }

        public IInventory GetInventory() {
            return Player.Inventory;
        }


        public void LoadSceneComplete(GameStateTypes gameState) {
            _popupQueue = new Queue<UIBasePopups>();
            _uiWindows = FindObjectsOfType<UIBaseWindows>();

            foreach (var item in _uiWindows) {
                item.Construct(this);
            }
            _uiPopups= FindObjectsOfType<UIBasePopups>();
            foreach (var item in _uiPopups) {
                item.Construct(this);
            }
            switch (gameState) {
                case GameStateTypes.Loading:
                    Debug.Log("Нет варианта для Loading");
                    break;
                case GameStateTypes.Menu:
                    ShowWindow(UIWindowsType.StartMenu);
                    //ShowPopup(UIPopupType.None);
                    ClosePopups();
                    Debug.Log($"UIResData Data GameController == null: {Data.GameController==null}");
                    break;
                case GameStateTypes.Game:
                    ShowPopup(UIPopupType.None);
                    ShowWindow(UIWindowsType.HUD);
                    ClosePopups();
                    break;
                case GameStateTypes.Pause:
                    break;
                default:
                    Debug.Log("Нет варианта для Default");
                    break;
            }


        }

        public void ShowWindow(UIWindowsType windowType) {
            foreach (var window in _uiWindows) {
                window.gameObject.SetActive(false);
            }
            foreach (var window in _uiWindows) {
                if (window.idUIWindowsType == windowType) {
                    window.gameObject.SetActive(true);
                }
            }
        }

        public void ShowPopup(UIPopupType popupType) {
            foreach (var popup in _uiPopups) {
                if (popup.idUIPopupType == popupType) {
                    if (popupType == UIPopupType.None) return;

                    // Если очередь пуста или первый в очереди попап уже активен, показываем окно сразу
                    if (_popupQueue.Count == 0) {
                        popup.gameObject.SetActive(true);
                    }

                    // Добавляем окно в очередь, если его там еще нет
                    if (!_popupQueue.Contains(popup)) {
                        _popupQueue.Enqueue(popup);
                    }
                }
            }
        }
        public void HandlePopupAfterHide(UIBasePopups popup) {
            // Удаляем окно из очереди
            if (_popupQueue.Contains(popup)) {
                _popupQueue.Dequeue();
            }

            // Если в очереди есть еще окна, показываем следующее
            if (_popupQueue.Count > 0) {
                _popupQueue.Peek().gameObject.SetActive(true);
            }
        }


        public void ClosePopups() {
            if( _uiPopups == null ) return;
            foreach (var popup in _uiPopups) {
                
                    popup.gameObject.SetActive(false);
               
            }
        }
        #region MenuScene
        public void PlayButtonInSceneMenu(SceneNames scene) {
            GameController.PlayButtonInSceneMenu(scene); 
        }

        #endregion

        #region GameScene

        public void OnFireButton() {
            GameController.RD.Player.StartFire();
        }

        public void OnInventoryButton() {
            Debug.Log("Inventory open!");
            ShowWindow(UIWindowsType.Inventory);
        }

        #endregion

    }
}