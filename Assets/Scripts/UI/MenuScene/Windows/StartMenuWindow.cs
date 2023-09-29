using Assets.Scripts.UI.Base;

namespace Assets.Scripts.UI.MenuScene.Windows {
    public class StartMenuWindow : UIBaseWindows {

        void Awake() {
            idUIWindowsType = UIWindowsType.StartMenu;
        }
        public void PlayButton() {
            _controller.PlayButtonInSceneMenu();
        }
    }
}