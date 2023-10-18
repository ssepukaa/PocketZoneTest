using Assets.Scripts.UI.Base;
using UnityEngine;

namespace Assets.Scripts.UI.GameScene.Popups {
    // ���������� ������������ ���� � ���������� �������
    public class UITaskCompletePopup : UIBasePopups {
        private void Awake() {
            idUIPopupType = UIPopupType.TaskComplete;
        }
        private void OnEnable() {
            Time.timeScale = 0f;

        }

        private void OnDisable() {
            Time.timeScale = 1f;
        
        }
        public void OnCloseButton() {
            _controller.Player.AfterMissionCompleteButton();

        }
    }
}
