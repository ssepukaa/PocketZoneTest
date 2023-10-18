using System.Collections;
using Assets.Scripts.UI.Base;
using UnityEngine;

namespace Assets.Scripts.UI.GameScene.Popups {
    // Реализация всплывающего окна о Перезарядке
    public class UIReloadingPopup: UIBasePopups {
        CanvasGroup _canvasGroup; // Ссылка на ваш CanvasGroup
        float delay = 2f; // Задержка перед исчезновением

        float fadeDuration = 1f; // Продолжительность плавного исчезновения
        private void Awake() {
            idUIPopupType = UIPopupType.Reloading;
            _canvasGroup = GetComponent<CanvasGroup>();
        }
        private void OnEnable() {
            if(_controller == null || _controller.Weapon == null) return;
            ShowAndHideWindow();
        }

        public void ShowAndHideWindow() {
            var duration = 0f;
            if (_controller == null || _controller.Weapon == null) {
                duration = delay;
            }
            else {
                duration = _controller.Weapon.TimeReloading();
            }
            StartCoroutine(HideWindowAfterDelay(duration));
        }

        private IEnumerator HideWindowAfterDelay(float duration) {
            delay = duration;
            fadeDuration = delay * 0.5f;
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
