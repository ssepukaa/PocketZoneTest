namespace Assets.Scripts.InventoryObject.Abstract {
    // Состояние предмета
    public interface IInventoryItemState {
        int Amount { get; set; }
        bool IsEquipped { get; set; }
    }
}
