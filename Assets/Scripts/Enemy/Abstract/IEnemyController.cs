using Assets.Scripts.Infra.Game.Abstract;
using Assets.Scripts.Player.Abstract;
using UnityEngine;

namespace Assets.Scripts.Enemy.Abstract {
    public interface IEnemyController {
        public void Construct(IGameController  gameController);

        Vector2 Position { get; }
        IPlayerController TargetEnemy { get; set; }
    }
}
