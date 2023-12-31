﻿using System;

namespace Assets.Scripts.Main.Game.SaveLoad {
    // Класс для сохранения/загрузки: слоты

    [Serializable]
    public class PlayerSaveData {
        public bool IsCompleteGame1;
        public bool IsCompleteGame2;
        public InventoryItemSaveData[] InventorySlots;
        public InventoryItemSaveData ClipSlot;
        public InventoryItemSaveData WeaponSlot;
        public int InventoryCapacity;
        public readonly int BaseInventoryCapacity = 16;
    }
}
