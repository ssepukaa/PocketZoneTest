using System.Collections;
using Assets.Scripts.UI.Base;
using UnityEngine;

namespace Assets.Scripts.UI.GameScene.Windows {
    public class UIHUDWindow : UIBaseWindows {

        private void Awake() {
            idUIWindowsType = UIWindowsType.HUD;
        }

        public void OnOpenInventoryButton() {
            _controller.OnInventoryButton();
        }

        public void FireButton() {
            _controller.OnFireButton();
        }
       
    }
}