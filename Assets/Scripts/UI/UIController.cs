using Assets.Scripts.Infra.Boot;
using Assets.Scripts.Infra.Game;
using Assets.Scripts.Infra.Game.Abstract;
using Assets.Scripts.InventoryObject.Abstract;
using Assets.Scripts.Player.Abstract;
using Assets.Scripts.UI.Base;
using Assets.Scripts.UI.Data;
using Assets.Scripts.Weapon;
using UnityEngine;

namespace Assets.Scripts.UI {
    public enum UIWindowsType {
        StartMenu, Inventory, HUD,
    }
    public enum UIPopupType { None, TaskComplete}
    public class UIController : MonoBehaviour, IUIController {
        public UIResData Data;
        public IWeaponController Weapon => GameController.RD.Player.Weapon;
        public IPlayerController Player => GameController.RD.Player;

        public IBootstrapper Bootstrapper { get => Data.Bootstrapper; set => Data.Bootstrapper = value; }

        public IGameController GameController { get => Data.GameController; set => Data.GameController = value; }

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

        public IInventory GetInventory() {
            return Player.Inventory;
        }


        public void LoadSceneComplete(GameStateTypes gameState) {
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
                    popup.gameObject.SetActive(true);
                }
            }
        }
        public void ClosePopups() {
            if( _uiPopups == null ) return;
            foreach (var popup in _uiPopups) {
                
                    popup.gameObject.SetActive(false);
               
            }
        }
        #region MenuScene
        public void PlayButtonInSceneMenu() {
            GameController.PlayButtonInSceneMenu(); 
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