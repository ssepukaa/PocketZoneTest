using Assets.Scripts.Infra.Game;
using Assets.Scripts.InventoryObject;
using Assets.Scripts.UI;

namespace Assets.Scripts.Player {
    public interface IPlayerController {
        public void Construct(IGameController gameController, IUIController uiController);
        public Inventory GetInventory();
    }
}