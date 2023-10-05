using System;
using System.Collections;
using Assets.Scripts.Infra.Boot;
using Assets.Scripts.Infra.Game;
using Assets.Scripts.InventoryObject;
using Assets.Scripts.InventoryObject.Abstract;
using Assets.Scripts.UI.Base;
using Assets.Scripts.UI.Data;
using UnityEngine;

namespace Assets.Scripts.UI {
    public enum UIWindowsType { StartMenu, Inventory, HUD, 
    }
    public enum UIPopupType{None,}
    public class UIController : MonoBehaviour, IUIController {
        
        private UIBaseWindows[] _uiWindows;
        private UIBasePopups[] _uiPopups;
        public UIModelData _md;
        public UIResourceData _rd;
        



        void Awake() {
            DontDestroyOnLoad(this);
        }


        public void Construct(Bootstrapper bootstrapper, IGameController gameController) {
            _rd._bootstrapper = bootstrapper;
            _rd._gameController = gameController;
            _rd._bootstrapper.InitUIComplete();
        }

        public void SetInventory(IInventory inventory) {
            _rd.Inventory = inventory;
        }

       

        public IInventory GetInventory() {
            return _rd.Inventory;
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
                    ShowPopup(UIPopupType.None);
                    break;
                case GameStateTypes.Game:
                    ShowWindow(UIWindowsType.HUD);
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
                    if(popupType == UIPopupType.None) return;
                    popup.gameObject.gameObject.SetActive(true);
                }
            }
        }

        #region MenuScene

        public void PlayButtonInSceneMenu() {
            _rd._gameController.PlayButtonInSceneMenu();
        }

        #endregion

        #region GameScene

        public void OnFireButton() {
            _rd._gameController.RD.Player.StartFire();
        }

        public void OnInventoryButton() {
            Debug.Log("Inventory open!");
            ShowWindow(UIWindowsType.Inventory);
        }

        #endregion

    }
}