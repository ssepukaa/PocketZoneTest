using Assets.Scripts.InventoryObject.Abstract;
using Assets.Scripts.Main.Boot;
using Assets.Scripts.Main.Game.Data;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Main.Game.Abstract {
    public interface IGameController {
        IGameResourceData RD { get; }
        Transform ControllerTransform { get; }
        IGameMode GameMode { get; set; }
        void Construct(Bootstrapper bootstrap, IUIController uiController);
        void LoadSceneComplete(GameStateTypes gameState);
        void PlayButtonInSceneMenu(SceneNames scene);
        void CreateLoot(object sender, Vector2 transformPosition, IInventoryItemInfo itemInfo, int amount);
        void CreateBullet(object sender,Transform transform, IInventoryItemInfo info, int amount);
        void TaskCompleted();
        void StartLoadSceneCoroutine(SceneNames nameScene, GameStateTypes stateType);
        void SaveDataPlayer();
        bool GetIsFirstLevelComplete();
    }
}