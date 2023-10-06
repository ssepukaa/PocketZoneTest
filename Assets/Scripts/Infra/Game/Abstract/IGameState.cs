namespace Assets.Scripts.Infra.Game.Abstract {
    public interface IGameState {
        public void SetState(GameStateTypes gameState);
        public GameStateTypes GetGameState();
    }
}