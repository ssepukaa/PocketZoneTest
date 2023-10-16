using System;
using Assets.Scripts.InventoryObject.Abstract;
using Assets.Scripts.InventoryObject.Data;

namespace Assets.Scripts.Weapon {
    public interface IWeaponController {
        event Action<int, int> OnFiredChangedEnvent;
        float TimeReloading();

    }
}