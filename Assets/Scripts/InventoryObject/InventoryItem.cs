using Assets.Scripts.InventoryObject.Abstract;
using Assets.Scripts.InventoryObject.Data;
using System;

namespace Assets.Scripts.InventoryObject {
    // ���������� �������� ���������
   
    public class InventoryItem: IInventoryItem{
        public IInventoryItemInfo Info => _info;

        public IInventoryItemState State => _state;

        public InventoryItemType ItemType => _info.ItemType;

        public int Amount{
            get {
                return this._state.Amount;
            }set{
                this._state.Amount = value;
            }
            
        }

        IInventoryItemInfo _info;

        IInventoryItemState _state;

        public InventoryItem(IInventoryItemInfo itemInfo) {
            _info = itemInfo;
            _state = new InventoryItemState();
            //_state.Amount = _info.MaxAmountSlot;
        }
    }
}


