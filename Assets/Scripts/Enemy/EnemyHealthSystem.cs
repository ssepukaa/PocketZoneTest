using Assets.Scripts.Enemy.Abstract;
using Assets.Scripts.Player.Abstract;
using System;
using Assets.Scripts.Components;

namespace Assets.Scripts.Enemy {
    public class EnemyHealthSystem: IHealth {
        private IEnemyController _controller;
        public event Action<float, float> OnHealthChangeEvent;

        public EnemyHealthSystem(IEnemyController controller) {
            _controller = controller;
            _controller.CurrentHealth = _controller.MaxHealth;

        }

        public void ApplyDamage(float damage) {
            _controller.CurrentHealth -= damage;
            CheckHealth();
            OnHealthChangeEvent?.Invoke(_controller.CurrentHealth, _controller.MaxHealth);
        }
        public void Refresh() {
            OnHealthChangeEvent?.Invoke(_controller.CurrentHealth, _controller.MaxHealth);
        }
        private void CheckHealth() {
            if (_controller.CurrentHealth <= 0f) {
                _controller.Death();
            }
        }
    }
}
