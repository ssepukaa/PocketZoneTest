namespace Assets.Scripts.Infra.Game.Abstract {
    public interface IGameState {
        void SetState(GameStateTypes gameState);
        GameStateTypes GetGameState();
    }
}