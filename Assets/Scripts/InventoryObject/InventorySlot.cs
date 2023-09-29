using Assets.Scripts.InventoryObject.Abstract;

namespace Assets.Scripts.InventoryObject {
    public class InventorySlot {
        private bool _isFull = false;
        private bool _isEmpty =true;
        private int _amount;
        private int _capacity;
        private IInventoryItem _item;
        public bool IsSelect;



        public bool GetIsFull() {
            return _isFull = !_isEmpty && _amount == _capacity;
        }

        public bool GetIsEmpty() {
            return _isEmpty = _item == null;
        }

        public int GetAmount() {
            return _amount = _isEmpty ? 0 : _item.GetState().GetAmount();
        }

        public int GetCapacity() {
            return _capacity;
        }
        public IInventoryItem GetItem() {
            return _item;
        }
        public void SetItem(IInventoryItem item) {
            if(!GetIsEmpty()) return;
            this._item = item;
            _capacity = item.GetInfo().maxAmountSlot;
        }

        public void Clear() {
           if(GetIsEmpty()) return;
           _item.GetState().SetAmount(0);
           _item = null;
        }

        public void Select() {
            IsSelect = true;
            UpdateUI();
        }
        public void Deselect() {
            IsSelect =false;
            UpdateUI();
        }
        public void UpdateUI() {
            // Логика обновления UI или других действий
        }

    }
}
