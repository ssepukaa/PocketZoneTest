using Assets.Scripts.Infra.Game.Abstract;

namespace Assets.Scripts.Infra.Game {
    class DamageSystem : IDamageSystem {
        private IGameController _c;
        public DamageSystem(IGameController controller) {
            _c = controller;
        }
    }
}