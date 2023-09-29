using System;

namespace Assets.Scripts.InventoryObject {
    [Serializable]
    public class InventoryItemState {

        private int _amount;
        public bool IsItemEquipped;


        public InventoryItemState() { 
            _amount = 0;
            IsItemEquipped = false;
        }

        public bool GetIsItemEquipped() {
            return IsItemEquipped;
        }
        public int GetAmount() {
            return _amount;
        }

        public void SetAmount(int amount) {
            _amount = amount;
        }

        public void AddAmount(int amount) {
            _amount += amount;
        }

        public void RemoveAmount(int amount) {
            _amount -= amount;
        }
    }
}