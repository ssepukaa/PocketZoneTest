using Assets.Scripts.InventoryObject.Data;

namespace Assets.Scripts.InventoryObject.Abstract {
    public interface IInventorySlot {
        bool IsFull { get; }
        bool IsEmpty { get; }
        bool IsSelect { get; }
        IInventoryItem Item { get; set; }
        InventoryItemType ItemType { get; }
        int GetItemAmount { get; }
        int Capacity { get; }

        void Select();
        void Deselect();
        void Clear();
    }
}
