using Assets.Scripts.UI.Base;
using TMPro;

namespace Assets.Scripts.UI.GameScene.Windows {
    public class UIHUDWindow : UIBaseWindows {
        public TMP_Text ClipAmountAmmoText;
        public TMP_Text TotalAmountAmmoText;
        public TMP_Text CollectedCoinsText;
        public TMP_Text TargetCoinsText;

        
        private void Awake() {
            idUIWindowsType = UIWindowsType.HUD;
        }

        public void OnOpenInventoryButton() {
            _controller.OnInventoryButton();
        }

        public void FireButton() {
            _controller.OnFireButton();
        }
        
        private void UpdateAmmoUI(int clipAmountAmmo, int totalAmountAmmo) {
            ClipAmountAmmoText.text = clipAmountAmmo.ToString();
            TotalAmountAmmoText.text = totalAmountAmmo.ToString();
        }

        private void UpdateCoinsUI(int collectedCoins, int targetCoins) {
            CollectedCoinsText.text = collectedCoins.ToString();
            TargetCoinsText.text = targetCoins.ToString();
        }

        void OnEnable() {
            if (_controller != null && _controller.Weapon != null) {
                _controller.Weapon.OnFiredChangedEnvent += UpdateAmmoUI;
                _controller.Player.OnMissionChangedEvent += UpdateCoinsUI;
                _controller.Player.UpdateTaskUI(); 
            }
               
        }
        
        void OnDisable() {
            if (_controller!=null && _controller.Weapon != null)
                _controller.Weapon.OnFiredChangedEnvent -= UpdateAmmoUI;
            _controller.Player.OnMissionChangedEvent -= UpdateCoinsUI;
            _controller.Player.UpdateTaskUI();
        }

    }
}