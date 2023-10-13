using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Components {
    public class UIHealthBar : MonoBehaviour
    {
        public Image fillImage;
        
        private IHealth _health;

        public bool Construct(IHealth health) {
            _health = health;
            _health.OnHealthChangeEvent += UpdateHealthBar;

            return true;
        }


        private void UpdateHealthBar(float current, float max) {
            float fillValue = current / max;
            fillImage.fillAmount = fillValue;
        }

        private void OnDestroy() {
           // _health.OnHealthChangeEvent -= UpdateHealthBar; 
        }
    }
}
