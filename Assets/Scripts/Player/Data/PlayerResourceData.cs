using Assets.Scripts.Infra.Game;
using Assets.Scripts.InventoryObject;
using Assets.Scripts.UI;
using Assets.Scripts.UI.InventoryUI;
using UnityEngine;

namespace Assets.Scripts.Player.Data {
    [CreateAssetMenu(fileName = "PlayerResourceData", menuName = "PocketZoneTest/PlayerResourceData")]
    public class PlayerResourceData : ScriptableObject {
        public IGameController GameController;
        public IUIController UIController;
        public Inventory inventory;
        public PlayerInputComp PlayerInput;
        public UIInventory UIInventory;
    }
}
