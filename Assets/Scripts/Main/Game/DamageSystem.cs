using Assets.Scripts.Main.Game.Abstract;

namespace Assets.Scripts.Main.Game {
    class DamageSystem : IDamageSystem {
        public DamageSystem(IGameController controller) {
            _c = controller;
        }

        IGameController _c;
    }
}