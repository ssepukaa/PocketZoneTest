using System;
using UnityEngine;

namespace Assets.Scripts.InventoryObject.Data {
    [CreateAssetMenu(fileName = "WeaponItemInfo", menuName = "PocketZoneTest/Info/Create New Weapon Info")]
    [Serializable]
    public class WeaponItemInfo : ScriptableObject {
        public ItemAmmoType AmmoType { get; }
        public int CapacityClip => _capacityClip;
        public float AttackRange => _attackRange;
        public float FireRate => _fireRate;
        public float DamageAmount => _damage;
        public float ReloadTime => _reloadTime;
        [SerializeField] ItemAmmoType _ammoType;
        [SerializeField] WeaponType _weaponType;
        [SerializeField] float _fireRate = -1f;
        [SerializeField] int _capacityClip = 30;
        [SerializeField] float _attackRange = 1f;
        [SerializeField] float _damage = 10f;
        [SerializeField] float _reloadTime = 1f;
    }
}