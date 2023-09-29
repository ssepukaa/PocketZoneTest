using System;
using Assets.Scripts.InventoryObject.Abstract;
using Assets.Scripts.InventoryObject.Data;

namespace Assets.Scripts.InventoryObject.Items {
    public abstract class InventoryItem: IInventoryItem{

        private InventoryItemInfo _info;
        private InventoryItemState _state;
        
        protected InventoryItem(InventoryItemInfo itemInfo) {
            _info = itemInfo;
            _state = new InventoryItemState();
        }

        public InventoryItemInfo GetInfo() {
            return _info;
        }

        public InventoryItemState GetState() {
            return _state;
        }
        public void SetAmount(int amount) {
            this._state.SetAmount(amount);
        }

        public Type GetItemType() {
            return this.GetType();
        }
        public IInventoryItem Clone() {
            Type itemType = InventoryItemInfo.itemTypeToClassMap[_info.inventoryItemType];
            var clonedItem = (IInventoryItem)Activator.CreateInstance(itemType, new object[] { _info });

            clonedItem.GetState().SetAmount(_state.GetAmount());
            return clonedItem;
        }

    }
}


