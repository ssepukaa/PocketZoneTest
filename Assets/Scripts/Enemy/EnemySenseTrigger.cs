using Assets.Scripts.Enemy.Abstract;
using Assets.Scripts.Player.Abstract;
using UnityEngine;

namespace Assets.Scripts.Enemy {
    public class EnemySenseTrigger : MonoBehaviour {
        public Transform WeaponTransform;
        IEnemyController _controller;
        Quaternion _initialWeaponRotation;
        Vector3 _initialWeaponScale;
        bool _isInitController;

        private void Start() {
            _initialWeaponRotation = WeaponTransform.rotation;
            _initialWeaponScale = WeaponTransform.localScale;
        }
        public bool Construct(IEnemyController controller) {
            _controller = controller;
            _isInitController = true;
            Debug.Log($"Init EnemyController OK! _controller != null: {_controller!=null}");
            return true;
        }

        public void Update() {
            if (!_isInitController) return;

            if (_controller.TargetEnemy != null) {

                // Вычислите направление от оружия к противнику
                Vector2 direction = _controller.TargetEnemy.Position - (Vector2)WeaponTransform.position;
                // Вычислите угол между оружием и противником
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                // Примените угол к оружию
                WeaponTransform.rotation = Quaternion.Euler(0, 0, angle);

                // Если противник находится слева от оружия, отразите оружие по оси Y
                if (direction.x < 0) {
                    WeaponTransform.localScale = new Vector3(WeaponTransform.localScale.x, -Mathf.Abs(WeaponTransform.localScale.y), WeaponTransform.localScale.z);
                } else {
                    WeaponTransform.localScale = new Vector3(WeaponTransform.localScale.x, Mathf.Abs(WeaponTransform.localScale.y), WeaponTransform.localScale.z);
                }

            } else {
                WeaponTransform.rotation = _initialWeaponRotation;
                WeaponTransform.localScale = _initialWeaponScale;

            }
        }
        private void OnTriggerEnter2D(Collider2D other) {
            IPlayerController visitor = other.GetComponent<IPlayerController>();
            if (visitor != null) {
                if (_controller.TargetEnemy == null) {
                    _controller.TargetEnemy = visitor;
                    _controller.DestinationSetter.target = _controller.TargetEnemy.TransformPlayer;
                }
            }
        }

    }
}
