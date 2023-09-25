namespace Assets.Scripts.Infra.Game {
    public class GameMode : IGameMode {
        private IGameController _c;

        public GameMode(IGameController controller) {
            _c=controller;
        }

    }
}