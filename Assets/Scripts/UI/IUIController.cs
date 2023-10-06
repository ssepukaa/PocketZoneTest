using Assets.Scripts.Infra.Boot;
using Assets.Scripts.Infra.Game;
using Assets.Scripts.Infra.Game.Abstract;
using Assets.Scripts.InventoryObject.Abstract;

namespace Assets.Scripts.UI {
    public interface IUIController {
        public void Construct(Bootstrapper bootstrapper, IGameController gameController);
        void LoadSceneComplete(GameStateTypes gameState);
        void PlayButtonInSceneMenu();
        public void ShowWindow(UIWindowsType windowType);
        void SetInventory(IInventory inventory);
        IInventory GetInventory();
        void OnFireButton();
        void OnInventoryButton();
    }
}