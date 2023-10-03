using Assets.Scripts.InventoryObject.Abstract;
using System;

namespace Assets.Scripts.InventoryObject {
    [Serializable]
    public class InventoryItemState: IInventoryItemState {

        private int _amount;
        private bool _isItemEquipped;
        public int Amount {
            get => _amount;
            set => _amount = value;
        }

        public bool IsEquipped {
            get => _isItemEquipped;
            set => _isItemEquipped = value;
        }

        public InventoryItemState() { 
            _amount = 0;
            _isItemEquipped = false;
        }

       

        

    }
}