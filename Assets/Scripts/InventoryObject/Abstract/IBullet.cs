namespace Assets.Scripts.InventoryObject.Abstract {
    public interface IBullet {
        public bool Construct(IInventoryItemInfo info, int amount);
    }
}