using System;

namespace Assets.Scripts.Components {
    public interface IHealth {
        public event Action<float,float> OnHealthChangeEvent;
    }
}