using Assets.Scripts.Main.Game.Abstract;

namespace Assets.Scripts.Main.Game {
    public enum GameStateTypes { Loading, Menu, Game, Pause, }

    public class GameState: IGameState {

        private IGameController _c;
        public GameStateTypes GameStateType;

        public GameState(IGameController gameController) {
            _c = gameController;
        }

        public void SetState(GameStateTypes gameState) {
            GameStateType = gameState;
        }

        public GameStateTypes GetGameState() {
            return GameStateType;
        }
        
    }
}