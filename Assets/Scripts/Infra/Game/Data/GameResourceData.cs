using Assets.Scripts.Infra.Boot;
using Assets.Scripts.Player;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Infra.Game.Data {
    [CreateAssetMenu(fileName = "GameResourceData", menuName = "PocketZoneTest/GameResourceData")]
    public class GameResourceData : ScriptableObject {
        public Bootstrapper Bootstrapper;
        public IUIController IUIController;
        public IPlayerController PlayerController;
        public GameMode GameMode;
        public GameState GameState;
    }
}