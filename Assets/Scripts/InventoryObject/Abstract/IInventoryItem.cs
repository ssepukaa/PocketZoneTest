using System;
using Assets.Scripts.InventoryObject.Data;

namespace Assets.Scripts.InventoryObject.Abstract {
    public interface IInventoryItem {
        InventoryItemInfo GetInfo();
        InventoryItemState GetState();
        Type GetItemType();
        void SetAmount(int amount);

        IInventoryItem Clone();
    }
}
