using UnityEngine;

namespace Assets.Scripts.UI.Base {
    // Базовый класс для высплывающих окон интерфейса
    public class UIBasePopups: MonoBehaviour {
        protected IUIController _controller;
        public UIPopupType idUIPopupType;
        public void Construct(IUIController controller) {
            _controller = controller;
           
        }
    }
}