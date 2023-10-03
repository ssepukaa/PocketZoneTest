using Assets.Scripts.Infra.Game;
using Assets.Scripts.InventoryObject.Abstract;
using Assets.Scripts.UI;

namespace Assets.Scripts.Player.Abstract {
    public interface IPlayerController {
        public void Construct(IGameController gameController, IUIController uiController);
        public bool CollectLoot(object sender, IInventoryItem item);
    }
}