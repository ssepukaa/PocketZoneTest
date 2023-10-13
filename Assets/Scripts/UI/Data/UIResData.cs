using Assets.Scripts.Infra.Boot;
using Assets.Scripts.Infra.Game.Abstract;
using UnityEngine;

namespace Assets.Scripts.UI.Data {
    [CreateAssetMenu(fileName = "UIResourceData", menuName = "PocketZoneTest/UIResourceData")]
    public class UIResData : ScriptableObject {
        public IBootstrapper Bootstrapper { get => _bootstrapper; set => _bootstrapper = value; }
        public IGameController GameController { get => _gameController; set => _gameController = value; }
        
        [SerializeField] IBootstrapper _bootstrapper;
        [SerializeField] IGameController _gameController;
    }
}
