using Assets.Scripts.InventoryObject.Abstract;
using Assets.Scripts.InventoryObject.Data;

namespace Assets.Scripts.InventoryObject {
    public class InventorySlot: IInventorySlot {
        private bool _isFull = false;
        private bool _isEmpty =true;
        private int _amount;
        private int _capacity;
        private IInventoryItem _item;
        private bool _isSelect;


        public bool IsSelect => _isSelect;
        public bool IsFull => _isFull = !_isEmpty && _amount == _capacity;
        public bool IsEmpty => _isEmpty = _item == null;
        public InventoryItemType ItemType => _isEmpty ? InventoryItemType.Empty : _item.ItemType;
        public int GetItemAmount =>_amount = _isEmpty ? 0 : _item.State.Amount;
        
        public int Capacity => _capacity;

        public IInventoryItem Item {
            get { return _item; }
            set { _item = value; }
        }
        
        public void Clear() {
           if(_isEmpty) return;
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
