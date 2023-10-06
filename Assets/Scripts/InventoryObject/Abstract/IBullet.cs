using Assets.Scripts.Infra.Game.Abstract;

namespace Assets.Scripts.InventoryObject.Abstract {
    public interface IBullet {
        public bool Construct(IDamageSystem listener, object sender, IInventoryItemInfo info, int amount);
    }
}