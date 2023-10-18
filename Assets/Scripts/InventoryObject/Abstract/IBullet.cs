using Assets.Scripts.Main.Game.Abstract;

namespace Assets.Scripts.InventoryObject.Abstract {
    public interface IBullet {
        bool Construct(/*IDamageSystem listener,*/ object sender, IInventoryItemInfo info, int amount);
    }
}