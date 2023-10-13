using System.Collections.Generic;
using Assets.Scripts.Enemy.Abstract;
using Assets.Scripts.Player.Abstract;
using UnityEngine;

namespace Assets.Scripts.Player {


    public class PlayerSenseTrigger : MonoBehaviour {

        IPlayerController _controller;
        float _radiusTrigger;
        public Transform WeaponTransform;
        bool _isInitController;
        CircleCollider2D _trigger;
        Quaternion _initialWeaponRotation;
        Vector3 _initialWeaponScale;


        private bool _isCheck = true;

        void Start() {
            _trigger = GetComponent<CircleCollider2D>();
            _radiusTrigger = _trigger.radius;
            _initialWeaponRotation = WeaponTransform.rotation;
            _initialWeaponScale = WeaponTransform.localScale;


        }
        public bool Construct(IPlayerController controller) {
            _controller = controller;
            _isInitController = true;
            return true;

        }

        private void Update() {
            if (!_isInitController) return;
            if (_isCheck) {
                LostTarget();
            }
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
            IEnemyController visitor = other.GetComponent<IEnemyController>();
            if (visitor != null) {
                if (_controller.TargetEnemy == null) {
                    SetTarget(visitor);
                }
            }
        }

        private void SetTarget(IEnemyController visitor) {
            Debug.Log("Setup target!");
            if (visitor != null) {
                _controller.TargetEnemy = visitor;
            }
            _isCheck = false;

        }

        private void LostTarget() {
            // Если вы хотите продолжить отслеживание других противников в триггере после того, как текущий противник покинул его:
            Collider2D[] visitorsInTrigger = Physics2D.OverlapCircleAll(transform.position, _radiusTrigger); // предполагая, что у вас есть радиус триггера
            var visitorsList = new List<IEnemyController>();
            foreach (Collider2D col in visitorsInTrigger) {
                var visitor = col.GetComponent<IEnemyController>();
                if (visitor != null) {
                    visitorsList.Add(visitor);
                }
            }
            UpdateNearestVisitors(visitorsList);
        }

        private void UpdateNearestVisitors(List<IEnemyController> visitors) {

            if (visitors == null || visitors.Count == 0) {
                return; // Нет противников в списке
            }

            IEnemyController nearestVisitor = visitors[0];
            float nearestDistance = Vector2.Distance(transform.position, nearestVisitor.Position);

            foreach (var visitor in visitors) {
                float currentDistance = Vector2.Distance(transform.position, visitor.Position);
                if (currentDistance < nearestDistance) {
                    nearestDistance = currentDistance;
                    nearestVisitor = visitor;
                }
            }

            SetTarget(nearestVisitor);

        }


        private void OnTriggerExit2D(Collider2D other) {
            IEnemyController visitor = other.GetComponent<IEnemyController>();
            if (visitor != null && visitor == _controller.TargetEnemy) {
                _controller.TargetEnemy = null;
                _isCheck = true;
            }

        }
    }
}