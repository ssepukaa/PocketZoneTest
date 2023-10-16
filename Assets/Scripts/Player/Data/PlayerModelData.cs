using System;
using Assets.Scripts.InventoryObject.Abstract;

namespace Assets.Scripts.Player.Data {
    
    public class PlayerModelData {
        public bool IsCompleteGame1;
        public bool IsCompleteGame2;
        public IInventorySlot[] _slots;
        public IInventorySlot _clipSlot;
        public IInventorySlot _weaponSlot;
        public IInventory _inventory;
        public int InventoryCapacity = 16;
        public readonly int BaseInventoryCapacity = 16;
    }
}
