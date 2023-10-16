namespace Assets.Scripts.InventoryObject.Abstract {
    public interface ILootContainer {
        IInventoryItem TryLootCollect();
        public void LootCollected();
    }
}