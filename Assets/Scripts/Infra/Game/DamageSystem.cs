using Assets.Scripts.Infra.Game.Abstract;

namespace Assets.Scripts.Infra.Game {
    class DamageSystem : IDamageSystem {
        public DamageSystem(IGameController controller) {
            _c = controller;
        }

        IGameController _c;
    }
}