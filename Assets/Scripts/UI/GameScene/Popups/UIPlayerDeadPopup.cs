using Assets.Scripts.UI.Base;
using UnityEngine;

namespace Assets.Scripts.UI.GameScene.Popups {
    // Реализация всплывающего окна о Смерти игрока
    public class UIPlayerDeadPopup : UIBasePopups {
        private void Awake() {
            idUIPopupType = UIPopupType.PlayerDead;
        }
        private void OnEnable() {
            Time.timeScale = 0f;

        }

        private void OnDisable() {
            Time.timeScale = 1f;
            
        }
        public void OnCloseButton() {
            _controller.Player.AfterDeathButton();

        }
    }
}
