using Assets.Scripts.Enemy.Abstract;
using UnityEngine;

namespace Assets.Scripts.Infra.Game.Abstract {
    public interface IController {
        public Vector2 Position { get; }
        
        public void TargetPosition(Vector2 position, bool isTarget);
    }
}
