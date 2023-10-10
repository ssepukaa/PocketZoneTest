﻿using Assets.Scripts.Infra.Game.Abstract;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Player.Data {
    [CreateAssetMenu(fileName = "PlayerResourceData", menuName = "PocketZoneTest/PlayerResourceData")]
    public class PlayerResourceData : ScriptableObject {
        public IGameController GameController;
        public IUIController UIController;
        public float MaxBaseHealth = 100f;
        public int CapacityInventory = 16;

       
    }
}
