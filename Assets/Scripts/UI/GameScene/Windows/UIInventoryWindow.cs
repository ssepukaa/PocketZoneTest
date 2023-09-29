using Assets.Scripts.UI.Base;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

namespace Assets.Scripts.UI.GameScene.Windows {
    public class UIInventoryWindow : UIBaseWindows {

        //[SerializeField] private Button _closeWindowButton;

        private void Awake() {
            idUIWindowsType = UIWindowsType.Inventory;
        }

        public void OnCloseButton() {
            _controller.ShowWindow(UIWindowsType.HUD);
        }
    }
}