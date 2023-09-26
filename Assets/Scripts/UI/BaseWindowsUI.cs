using UnityEngine;

namespace Assets.Scripts.UI {
    public class BaseWindowsUI : MonoBehaviour {
        protected IUIController _controller;
        public UIWindowsType idUiWindowsType;
        public void Construct(IUIController controller) {
            _controller = controller;
            //gameObject.SetActive(false);
        }

    }
}