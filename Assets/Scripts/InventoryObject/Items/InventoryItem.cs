using System;
using Assets.Scripts.InventoryObject.Abstract;
using Assets.Scripts.InventoryObject.Data;

namespace Assets.Scripts.InventoryObject.Items {
    public class InventoryItem: IInventoryItem{

        private IInventoryItemInfo _info;
        private IInventoryItemState _state;


        public InventoryItem(IInventoryItemInfo itemInfo) {
            _info = itemInfo;
            _state = new InventoryItemState();
        }
        
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
        

    }
}

