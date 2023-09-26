using System;
using Assets.Scripts.Infra.Game;
using UnityEngine;

namespace Assets.Scripts.Player.Data {
    [Serializable]
    public class PlayerModelData : MonoBehaviour {
        public GameStateTypes gameState;
    }
}
