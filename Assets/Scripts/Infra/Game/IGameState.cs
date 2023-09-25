namespace Assets.Scripts.Infra.Game {
    public interface IGameState {
        public void SetState(GameStateTypes gameState);
        public GameStateTypes GetGameState();
    }
}