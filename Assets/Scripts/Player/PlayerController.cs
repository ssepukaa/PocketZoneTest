using Assets.Scripts.Infra.Game;
using Assets.Scripts.InventoryObject;
using Assets.Scripts.Player.Data;
using Assets.Scripts.UI;
using Assets.Scripts.UI.InventoryUI;
using UnityEngine;

namespace Assets.Scripts.Player {
    public class PlayerController : MonoBehaviour, IPlayerController {
      

        public PlayerResourceData rd;
        

        public void Construct(IGameController gameController, IUIController uiController) {
            rd.GameController = gameController;
            rd.UIController = uiController;
            rd.inventory = new Inventory(16);
            rd.UIInventory = FindObjectOfType<UIInventory>();
            rd.UIInventory.Construct(this);
            rd.inventory.UIInventory =  rd.UIInventory;
            rd.PlayerInput = GetComponent<PlayerInputComp>();
            rd.PlayerInput.Construct(this);
        }

        public Inventory GetInventory() {
            return rd.inventory;
        }

        public void OpenInventory() {
           rd.UIController.ShowWindow(UIWindowsType.Inventory);
        }
    }
}