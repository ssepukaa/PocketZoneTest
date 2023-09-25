﻿using Assets.Scripts.Infra.Boot;
using Assets.Scripts.Infra.Game;

namespace Assets.Scripts.UI {
    public interface IUIController {
        public void Construct(Bootstrapper bootstrapper, IGameController gameController);
        void LoadMenuComplete();
    }
}