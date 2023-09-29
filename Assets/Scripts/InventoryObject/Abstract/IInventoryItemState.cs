namespace Assets.Scripts.InventoryObject.Abstract {
    public interface IInventoryItemState {
        int amount { get; set; }
        bool isEquipped { get; set; }
    }
}
