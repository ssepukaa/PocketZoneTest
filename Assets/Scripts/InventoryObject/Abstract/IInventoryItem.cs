﻿using System;
using Assets.Scripts.InventoryObject.Data;

namespace Assets.Scripts.InventoryObject.Abstract {
    // предмет инвентаря
    public interface IInventoryItem {
        IInventoryItemInfo Info { get; }
        IInventoryItemState State { get; }
        InventoryItemType ItemType { get; }
        int Amount { get; set; } 

    }
}
