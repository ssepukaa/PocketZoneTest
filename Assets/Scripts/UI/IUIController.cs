using Assets.Scripts.Infra.Boot;
using Assets.Scripts.Infra.Game;
using Assets.Scripts.InventoryObject;
using Assets.Scripts.InventoryObject.Abstract;
using Assets.Scripts.UI.Data;

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