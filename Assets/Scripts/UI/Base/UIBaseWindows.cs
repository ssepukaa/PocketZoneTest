﻿using UnityEngine;

namespace Assets.Scripts.UI.Base {
    public class UIBaseWindows : MonoBehaviour {
        protected IUIController _controller;
        public UIWindowsType idUIWindowsType;
        public virtual void Construct(IUIController controller) {
            _controller = controller;
            //gameObject.SetActive(false);
        }

    }
}