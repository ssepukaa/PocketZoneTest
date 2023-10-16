using System;
using Assets.Scripts.InventoryObject.Data;

namespace Assets.Scripts.Infra.Game.SaveLoad {
    [Serializable]
    public class InventoryItemSaveData {
        public InventoryItemType ItemType;
        public int Amount;
    }
}