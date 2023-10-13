using Assets.Scripts.Infra.Boot;
using Assets.Scripts.Infra.Game;
using Assets.Scripts.Infra.Game.Abstract;
using Assets.Scripts.InventoryObject.Abstract;
using Assets.Scripts.Player.Abstract;
using Assets.Scripts.Weapon;

namespace Assets.Scripts.UI {
    public interface IUIController {
        void Construct(IBootstrapper bootstrapper, IGameController gameController);
        void LoadSceneComplete(GameStateTypes gameState);
        void PlayButtonInSceneMenu();
        void ShowWindow(UIWindowsType windowType);
        void ShowPopup(UIPopupType popupType);
        IInventory GetInventory();
        void OnFireButton();
        void OnInventoryButton();
        IWeaponController Weapon { get; }
        IPlayerController Player { get; }
    }
}