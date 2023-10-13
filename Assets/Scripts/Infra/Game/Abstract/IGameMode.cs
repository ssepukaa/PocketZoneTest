using Assets.Scripts.Player.Abstract;

namespace Assets.Scripts.Infra.Game.Abstract {
    public interface IGameMode {
        void ChangeState(SceneNames sceneName);
        void InitTaskPlayer(IPlayerController player);
        void CollectCoin(IPlayerController player);
    }
}