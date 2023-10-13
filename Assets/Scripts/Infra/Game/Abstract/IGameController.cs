using Assets.Scripts.Infra.Boot;
using Assets.Scripts.Infra.Game.Data;
using Assets.Scripts.InventoryObject.Abstract;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Infra.Game.Abstract {
    public interface IGameController {
        IGameResourceData RD { get; }
        Transform ControllerTransform { get; }
        IGameMode GameMode { get; set; }
        void Construct(Bootstrapper bootstrap, IUIController uiController);
        void LoadSceneComplete(GameStateTypes gameState);
        void PlayButtonInSceneMenu();
        void CreateLoot(object sender, Vector2 transformPosition, IInventoryItemInfo itemInfo, int amount);
        void CreateBullet(object sender,Transform transform, IInventoryItemInfo info, int amount);
        void TaskCompleted();
    }
}