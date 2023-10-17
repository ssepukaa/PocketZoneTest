namespace Assets.Scripts.Main.Game.Abstract {
    public interface IGameState {
        void SetState(GameStateTypes gameState);
        GameStateTypes GetGameState();
    }
}