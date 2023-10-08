using Assets.Scripts.Enemy.Abstract;
using Assets.Scripts.Infra.Game.Abstract;
using Assets.Scripts.InventoryObject.Abstract;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Player.Abstract {
    public interface IPlayerController {
        public void Construct(IGameController gameController, IUIController uiController);
        public bool CollectLoot(object sender, IInventoryItem item);
        void StartFire();
        IEnemyController TargetEnemy { get; set; }
        Vector2 Position { get; }
    }
}