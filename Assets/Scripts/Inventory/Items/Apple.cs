using System;
using Assets.Scripts.Inventory.Abstract;

namespace Assets.Scripts.Inventory.Items {
    public class Apple: IInventoryItem {
        public bool isEquipped { get; set; }

        public Type type => GetType();

        public int maxItemsInInventorySlot { get; }

        public int amount { get; set; }

        public Apple(int maxItemsInInventorySlot) {
            this.maxItemsInInventorySlot = maxItemsInInventorySlot;
        }

        public IInventoryItem Clone() {
            return new Apple(maxItemsInInventorySlot) {
                amount = this.amount
            };
        }
    }
}
