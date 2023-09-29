using UnityEngine;

namespace Assets.Scripts.UI.Base {
    public class UIBasePopups: MonoBehaviour {
        protected IUIController _controller;
        public UIPopupType idUIPopupType;
        public void Construct(IUIController controller) {
            _controller = controller;
           
        }
    }
}