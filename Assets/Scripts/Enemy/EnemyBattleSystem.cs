using Assets.Scripts.Enemy.Abstract;
using UnityEngine;

namespace Assets.Scripts.Enemy {
    public class EnemyBattleSystem : MonoBehaviour {

        IEnemyController _controller;
        float _attackCooldown;
        bool _canAttack = true;
        bool _isInit;

        public bool Construct(IEnemyController controller) {
            _controller = controller;

            _isInit = true;
            return true;
        }


        private void Update() {
            if (!_isInit) { return; }
            if (_controller.TargetEnemy == null) return;

            // Проверка расстояния до цели
            float distanceToTarget = Vector2.Distance(transform.position, _controller.TargetEnemy.Position);
            if (distanceToTarget <= _controller.WeaponSlot.Item.Info.WeaponInfo.AttackRange && _canAttack) {
                Attack();
            }
        }

        private void Attack() {
            // Здесь вы можете добавить логику атаки, например, нанесение урона цели
            Debug.Log("Attacking target!");
            _controller.TargetEnemy.ApplyDamage(this, _controller.WeaponSlot.Item.Info.WeaponInfo.DamageAmount);
            // Начать отсчет времени до следующей атаки
            _canAttack = false;
            _attackCooldown = _controller.WeaponSlot.Item.Info.WeaponInfo.FireRate;
            Invoke("ResetAttackCooldown", _attackCooldown);
        }

        private void ResetAttackCooldown() {
            _canAttack = true;
        }
    }
}