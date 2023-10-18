using System;
using Assets.Scripts.InventoryObject.Data;

namespace Assets.Scripts.Main.Game.SaveLoad {
    // Класс для сохранения/загрузки: состояние слотов
    [Serializable]
    public class InventoryItemSaveData {
        public InventoryItemType ItemType;
        public int Amount;
    }
}