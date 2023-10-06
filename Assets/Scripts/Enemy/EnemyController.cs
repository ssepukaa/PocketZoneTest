using System.Collections;
using Assets.Scripts.Components;
using Assets.Scripts.Enemy.Abstract;
using Assets.Scripts.Enemy.DataRes;
using Assets.Scripts.Infra.Game.Abstract;
using UnityEngine;

namespace Assets.Scripts.Enemy {
    public class EnemyController : MonoBehaviour, IEnemyController, IController, IDamageable {
        public EnemyResourceData _rd;
        private IGameController _gameController;
        private SpriteRenderer _spriteRenderer;
        //private PlayerSenseTrigger _playerSenseTrigger;
       

        public void Construct(IGameController gameController) {
            _gameController = gameController;
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _spriteRenderer.sprite = _rd._spriteEnemy;
          //  _playerSenseTrigger = GetComponentInChildren<PlayerSenseTrigger>();
          //  _playerSenseTrigger.Construct(this);

        }

        public Vector2 Position => transform.position;
        public void TargetPosition(Vector2 position, bool isTarget) {
            throw new System.NotImplementedException();
        }

        public void TakeDamage(float ammoInfoDamage) {
            Debug.Log("Get damage from player!");
        }
    }
}
