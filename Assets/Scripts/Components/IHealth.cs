using System;

namespace Assets.Scripts.Components {
    public interface IHealth {
        event Action<float,float> OnHealthChangeEvent;
    }
}