using Assets.Scripts.Main.Game.Abstract;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Player.Data {
    // Ссылки и ресурсы для игрока

    [CreateAssetMenu(fileName = "PlayerResourceData", menuName = "PocketZoneTest/PlayerResourceData")]
    public class PlayerResourceData : ScriptableObject {
        public IGameController GameController;
        public IUIController UIController;
        public float MaxBaseHealth = 100f;
        public int CapacityInventory = 16;

       
    }
}
