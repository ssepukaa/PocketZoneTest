using UnityEngine;

namespace Assets.Scripts.InventoryObject.Data {
    [CreateAssetMenu(fileName = "WeaponItemInfo", menuName = "PocketZoneTest/Info/Create New Weapon Info")]
    public class WeaponItemInfo : ScriptableObject {
        [SerializeField] private ItemAmmoType _ammoType;
        [SerializeField] private WeaponType _weaponType;
        [SerializeField] private float _fireRate = -1f;
        [SerializeField] private int _capacityClip = 30;
        [SerializeField] private float _attackRange = 1f;
        [SerializeField] private float _damage = 10f;
        public int CapacityClip => _capacityClip;
        public ItemAmmoType AmmoType { get; }

        public float AttackRange => _attackRange;
        public float FireRate => _fireRate;
        public float DamageAmount => _damage;
    }
}