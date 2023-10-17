using Assets.Scripts.Enemy.Abstract;
using Assets.Scripts.Player.Abstract;
using UnityEngine;

namespace Assets.Scripts.Enemy {
    public class EnemySenseTrigger : MonoBehaviour {
        public Transform WeaponTransform;
        public Transform PlayerBodyTransform;
        IEnemyController _controller;

        Quaternion _initialWeaponRotation;
        Vector3 _initialWeaponScale;
        Quaternion _initialPlayerRotation;
        Vector3 _initialPlayerScale;

        bool _isInitController;

        private void Start() {
            _initialWeaponRotation = WeaponTransform.rotation;
            _initialWeaponScale = WeaponTransform.localScale;

            _initialPlayerRotation = PlayerBodyTransform.rotation;
            _initialPlayerScale = PlayerBodyTransform.localScale;
        }
        public bool Construct(IEnemyController controller) {
            _controller = controller;
            _isInitController = true;
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

                // Отражение игрока по оси X
                if (direction.x < 0) {
                    PlayerBodyTransform.localScale = new Vector3(-Mathf.Abs(PlayerBodyTransform.localScale.x),
                        PlayerBodyTransform.localScale.y, PlayerBodyTransform.localScale.z);
                    WeaponTransform.localScale = new Vector3(-Mathf.Abs(WeaponTransform.localScale.x),
                        -Mathf.Abs(WeaponTransform.localScale.y), WeaponTransform.localScale.z);
                } else {
                    PlayerBodyTransform.localScale = new Vector3(Mathf.Abs(PlayerBodyTransform.localScale.x),
                        PlayerBodyTransform.localScale.y, PlayerBodyTransform.localScale.z);
                    WeaponTransform.localScale = new Vector3(Mathf.Abs(WeaponTransform.localScale.x),
                        Mathf.Abs(WeaponTransform.localScale.y), WeaponTransform.localScale.z);
                }

            } else {
                WeaponTransform.rotation = _initialWeaponRotation;
                PlayerBodyTransform.rotation = _initialPlayerRotation;
                PlayerBodyTransform.localScale = _initialPlayerScale;
                WeaponTransform.localScale = _initialWeaponScale;
            }
        }
        
        private void OnTriggerEnter2D(Collider2D other) {
            if (!_isInitController) return;

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
