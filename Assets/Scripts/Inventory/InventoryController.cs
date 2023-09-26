using System.Collections;
using Assets.Scripts.Inventory.Abstract;
using Assets.Scripts.Inventory.Items;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Inventory {
    public class InventoryController : MonoBehaviour {

        private IInventory _inventory;
        public InputAction AddItem;
        public InputAction RemoveItem;

        private void Awake() {
            var inventoryCapacity = 10;
            _inventory = new InventoryWithSlots(inventoryCapacity);
            Debug.Log($"Inventory initialized, capacity: {inventoryCapacity}");
        }

        private void OnEnable() {
            AddItem.Enable();
            RemoveItem.Enable();
            AddItem.performed += OnAddItemPerformed;
            RemoveItem.performed += OnRemoveItemPerformed;
       }

        private void OnDisable() {
            AddItem.Enable();
            RemoveItem.Enable();
            AddItem.performed -= OnAddItemPerformed;
            RemoveItem.performed -= OnRemoveItemPerformed;
        }

       private void OnAddItemPerformed(InputAction.CallbackContext obj) {
           AddRandomApples();
       }

       private void OnRemoveItemPerformed(InputAction.CallbackContext obj) {
           RemoveRandomApples();
       }

       private void AddRandomApples() {
           var rCount = Random.Range(1, 5);
           var apple = new Apple(maxItemsInInventorySlot: 5);
           apple.amount = rCount;
           _inventory.TryToAdd(this, apple);
       }

       private void RemoveRandomApples() {
           var rCount = Random.Range(1, 10);
           _inventory.Remove(this, typeof(Apple), rCount);
       }
    }
}