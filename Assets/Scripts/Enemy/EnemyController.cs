using System.Collections;
using Assets.Scripts.Components;
using Assets.Scripts.Enemy.Abstract;
using Assets.Scripts.Enemy.DataRes;
using Assets.Scripts.Infra.Game.Abstract;
using Assets.Scripts.Player.Abstract;
using UnityEngine;

namespace Assets.Scripts.Enemy {
    public class EnemyController : MonoBehaviour, IEnemyController, IController, IDamageable {
        public EnemyResourceData RD;
        private IGameController _gameController;
        private SpriteRenderer _spriteRenderer;
        private EnemySenseTrigger _senseTrigger;
        public Vector2 Position => transform.position;

        public IPlayerController TargetEnemy {
            get => RD.Target;
            set => RD.Target = value;
        }


        public void Construct(IGameController gameController) {
            _gameController = gameController;
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _spriteRenderer.sprite = RD._spriteEnemy;
            _senseTrigger = GetComponentInChildren<EnemySenseTrigger>();
            _senseTrigger.Construct(this);

        }

        public void TargetPosition(Vector2 position, bool isTarget) {
            throw new System.NotImplementedException();
        }

        public void TakeDamage(float ammoInfoDamage) {
            Debug.Log("Get damage from player!");
        }
    }
}
