namespace Assets.Scripts.InventoryObject.Abstract {
    // Контейнер для лута для хранения на сцене
    public interface ILootContainer {
        IInventoryItem TryLootCollect();
        public void LootCollected();
    }
}