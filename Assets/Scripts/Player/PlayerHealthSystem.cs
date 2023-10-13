using System;
using Assets.Scripts.Components;
using Assets.Scripts.Player.Abstract;

namespace Assets.Scripts.Player {
    public class PlayerHealthSystem: IHealth {
        public event Action <float, float> OnHealthChangeEvent;
        IPlayerController _controller;

        public PlayerHealthSystem(IPlayerController playerController) {
            _controller = playerController;
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