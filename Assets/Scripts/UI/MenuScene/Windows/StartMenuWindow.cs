using Assets.Scripts.Main.Game;
using Assets.Scripts.UI.Base;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.MenuScene.Windows {
    public class StartMenuWindow : UIBaseWindows {
        public Button Button1;
        public Button Button2;

        void Awake() {
            idUIWindowsType = UIWindowsType.StartMenu;
        }
        public override void Construct(IUIController controller) {
           base.Construct(controller);
            UpdateUI();
        }
        private void UpdateUI() {
            Debug.Log("Update Menu Button");
            if (_controller.GetIsFirstLevelComplete()) {
                Button2.gameObject.SetActive(true);
                Debug.Log("LevelOneComplete");
            }
            else {
                Button2.gameObject.SetActive(false);
                Debug.Log("LevelOne Not Complete");
            }
        }
        public void PlayButton1() {
            _controller.PlayButtonInSceneMenu(SceneNames.Game1);
        }
        public void PlayButton2() {
            _controller.PlayButtonInSceneMenu(SceneNames.Game2);
        }

    }
}