namespace Assets.Scripts.Main.Boot {
    // Главный загрузчик
    public interface IBootstrapper {
        void InitGameComplete();
        void InitUIComplete();
    }
}