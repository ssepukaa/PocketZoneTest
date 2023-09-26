using Assets.Scripts.Infra.Boot;
using Assets.Scripts.Player;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Infra.Game.Data {
    [CreateAssetMenu(fileName = "GameResourceData", menuName = "PocketZoneTest/GameResourceData")]
    public class GameResourceData : ScriptableObject {
        public Bootstrapper _bootstrapper;
        public IUIController _ui;
        public IPlayerController _player;
        public GameMode _mode;
        public GameState _state;
    }
}