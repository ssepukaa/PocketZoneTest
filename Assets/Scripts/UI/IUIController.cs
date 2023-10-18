using Assets.Scripts.InventoryObject.Abstract;
using Assets.Scripts.Main.Boot;
using Assets.Scripts.Main.Game;
using Assets.Scripts.Main.Game.Abstract;
using Assets.Scripts.Player.Abstract;
using Assets.Scripts.UI.Base;
using Assets.Scripts.Weapon;

namespace Assets.Scripts.UI {
    // Итерфейс UI
    public interface IUIController {
        void Construct(IBootstrapper bootstrapper, IGameController gameController);
        void LoadSceneComplete(GameStateTypes gameState);
        void HandlePopupAfterHide(UIBasePopups popup);
        void PlayButtonInSceneMenu(SceneNames scene);
        void ShowWindow(UIWindowsType windowType);
        void ShowPopup(UIPopupType popupType);
        bool GetIsFirstLevelComplete();
        IInventory GetInventory();
        void OnFireButton();
        void OnInventoryButton();
        IWeaponController Weapon { get; }
        IPlayerController Player { get; }
    }
}