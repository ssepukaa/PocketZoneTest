using System;

namespace Assets.Scripts.Weapon {
    // Оружие
    public interface IWeaponController {
        event Action<int, int> OnFiredChangedEnvent;
        float TimeReloading();

    }
}