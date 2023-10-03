using Assets.Scripts.Infra.Boot;
using Assets.Scripts.Infra.Game.Data;
using Assets.Scripts.UI;

namespace Assets.Scripts.Infra.Game {
    public interface IGameController {
        public void Construct(Bootstrapper bootstrap, IUIController uiController);

        void LoadSceneComplete(GameStateTypes gameState);
        void PlayButtonInSceneMenu();
        IGameResourceData RD { get; }
    }
}