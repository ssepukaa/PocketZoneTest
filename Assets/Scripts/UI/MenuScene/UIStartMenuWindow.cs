using UnityEngine;

namespace Assets.Scripts.UI.MenuScene {
    public class UIStartMenuWindow : BaseWindowsUI {

        void Awake() {
            idUiWindowsType = UIWindowsType.StartMenu;
        }
        public void PlayButton() {
            _controller.PlayButtonInSceneMenu();
        }
    }
}