using Assets.Scripts.Infra.Boot;
using Assets.Scripts.Infra.Game;
using Assets.Scripts.InventoryObject;
using Assets.Scripts.InventoryObject.Abstract;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.UI.Data {
    [CreateAssetMenu(fileName = "UIResourceData", menuName = "PocketZoneTest/UIResourceData")]

    public class UIResourceData : ScriptableObject {
        public Bootstrapper _bootstrapper;
        public IGameController _gameController;
        public IInventory Inventory;
    }
}