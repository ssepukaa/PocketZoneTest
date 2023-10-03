namespace Assets.Scripts.InventoryObject.Abstract {
    public interface IInventoryItemState {
        int Amount { get; set; }
        bool IsEquipped { get; set; }
    }
}
