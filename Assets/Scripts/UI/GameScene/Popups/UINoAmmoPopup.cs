using System.Collections;
using Assets.Scripts.UI.Base;
using UnityEngine;

namespace Assets.Scripts.UI.GameScene.Popups {
    public class UINoAmmoPopup: UIBasePopups {
        CanvasGroup _canvasGroup; // Ссылка на ваш CanvasGroup

        float delay = 2f; // Задержка перед исчезновением

        float fadeDuration = 1f; // Продолжительность плавного исчезновения

        private void Awake() {
            idUIPopupType = UIPopupType.NoAmmo;
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        private void OnEnable() {
            ShowAndHideWindow();
        }

        public void ShowAndHideWindow() {
            StartCoroutine(HideWindowAfterDelay());
        }

        private IEnumerator HideWindowAfterDelay() {
            _canvasGroup.alpha = 1; // Показать окно
           // _canvasGroup.blocksRaycasts = true; // Сделать окно активным

            yield return new WaitForSeconds(delay); // Ждать заданное количество секунд

            float startTime = Time.time;
            while (Time.time < startTime + fadeDuration) {
                _canvasGroup.alpha = Mathf.Lerp(1, 0, (Time.time - startTime) / fadeDuration);
                yield return null;
            }

            _canvasGroup.alpha = 0; // Скрыть окно
            //_canvasGroup.blocksRaycasts = false; // Сделать окно неактивным
            gameObject.SetActive(false); // Отключение объекта после завершения корутины
            _controller.HandlePopupAfterHide(this);
        }
    }
}
