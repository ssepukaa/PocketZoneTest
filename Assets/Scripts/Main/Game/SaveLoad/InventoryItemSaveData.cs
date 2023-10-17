using System;
using Assets.Scripts.InventoryObject.Data;

namespace Assets.Scripts.Main.Game.SaveLoad {
    [Serializable]
    public class InventoryItemSaveData {
        public InventoryItemType ItemType;
        public int Amount;
    }
}