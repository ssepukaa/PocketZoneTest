using Assets.Scripts.InventoryObject.Abstract;
using System;

namespace Assets.Scripts.InventoryObject {
    [Serializable]
    public class InventoryItemState : IInventoryItemState {
        public int Amount { get => _amount; set => _amount = value; }

        public bool IsEquipped { get => _isItemEquipped; set => _isItemEquipped = value; }

        int _amount;
        bool _isItemEquipped = false;

        public InventoryItemState() {
            _amount = 1;
            _isItemEquipped = false;
        }
    }
}