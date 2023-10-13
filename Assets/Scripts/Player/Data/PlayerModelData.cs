using System;
using Assets.Scripts.Infra.Game;
using Assets.Scripts.InventoryObject;
using Assets.Scripts.InventoryObject.Abstract;

namespace Assets.Scripts.Player.Data {
    [Serializable]
    public class PlayerModelData {
        public bool IsCompleteGame1;
        public bool IsCompleteGame2;
        public IInventorySlot[] _slots;
        public IInventorySlot _clipSlot;
        public IInventorySlot _weaponSlot;
        public IInventory _inventory;
        public readonly int BaseInventoryCapacity = 16;
    }
}
