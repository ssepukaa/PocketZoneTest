using System;

namespace Assets.Scripts.Weapon {
    public interface IWeaponController {
        public event Action<int, int> OnFiredChangedEnvent;
    }
}