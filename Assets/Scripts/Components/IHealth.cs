using System;

namespace Assets.Scripts.Components {
    // Интерфейс для получения компонента здоровья у объектов при получении урона
    public interface IHealth {
        event Action<float,float> OnHealthChangeEvent;
    }
}