using Assets.Scripts.InventoryObject.Abstract;
using Assets.Scripts.InventoryObject.Data;
using System;

namespace Assets.Scripts.InventoryObject {
    [Serializable]
    public class InventorySlot : IInventorySlot {
        public InventoryItemType ItemType => _isEmpty ? InventoryItemType.Empty : _item.ItemType;
        public bool IsSelect => _isSelect;
        public bool IsEmpty => _isEmpty = _item == null || _item.Amount == 0;
        public int GetItemAmount => _amount = _isEmpty ? 0 : _item.State.Amount;
        //bool _isFull = false;
        bool _isEmpty = true;
        int _amount;
        IInventoryItem _item;
        bool _isSelect;

        // private int _capacity;
        // public int Capacity => _capacity;
        //public bool IsFull => _isFull = !_isEmpty && _amount == _capacity;
        public IInventoryItem Item {
            get { return _item; }
            set { _item = value; }
        }

        public void Clear() {
            if (_isEmpty) return;
            _isSelect = false;
            _item.Amount = 0;
            _item = null;
        }

        public void Select() {
            _isSelect = true;

        }
        public void Deselect() {
            _isSelect =false;

        }


    }
}
