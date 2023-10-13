using Assets.Scripts.InventoryObject.Data;

namespace Assets.Scripts.InventoryObject.Abstract {
    public interface IInventorySlot {
        IInventoryItem Item { get; set; }

        InventoryItemType ItemType { get; }

        bool IsEmpty { get; }

        bool IsSelect { get; }

        int GetItemAmount { get; }

        void Select();

        void Deselect();

        void Clear();
        //int Capacity { get; }
        //bool IsFull { get; }
    }
}
