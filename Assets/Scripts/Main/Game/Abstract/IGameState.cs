namespace Assets.Scripts.Main.Game.Abstract {
    // Состояние игры -в принципе в данный момент не используется
    public interface IGameState {
        void SetState(GameStateTypes gameState);
        GameStateTypes GetGameState();
    }
}