using Assets.Scripts.UI;
using Assets.Scripts.UI.Base;
using UnityEngine;

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
        

    }
}
