using Assets.Scripts.Infra.Boot;
using Assets.Scripts.Infra.Game.Data;
using Assets.Scripts.InventoryObject.Abstract;
using Assets.Scripts.Player;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Infra.Game {
    public interface IGameController {
        public void Construct(Bootstrapper bootstrap, IUIController uiController);

        void LoadSceneComplete(GameStateTypes gameState);
        void PlayButtonInSceneMenu();
        IGameResourceData RD { get; }
        void CreateLoot(object sender, Vector2 transformPosition, IInventoryItemInfo itemInfo, int amount);
        void CreateBullet(object sender,Transform transform, IInventoryItemInfo info, int amount);
    }
}