using Assets.Scripts.InventoryObject.Abstract;
using Assets.Scripts.Player;
using Assets.Scripts.Player.Abstract;
using UnityEngine;

namespace Assets.Scripts.Weapon {
    public class WeaponController : MonoBehaviour {
        private IPlayerController _c;
        private SpriteRenderer _spriteRenderer;
        private IInventoryItemInfo _itemInfo;
        private Muzzle _muzzle;

        private void Start() {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _muzzle = GetComponentInChildren<Muzzle>();
        }
        public void Construct(IPlayerController playerController) {
            _c = playerController;

        }

        public void UpdateWeapon(IInventoryItemInfo info) {
            _spriteRenderer.sprite = info.SpriteIcon;
            _itemInfo = info;
        }

        public Transform GetMuzzleTransform() {
            return _muzzle.transform;
        }

    }
}