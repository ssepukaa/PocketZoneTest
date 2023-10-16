using System;
using UnityEngine;

namespace Assets.Scripts.InventoryObject.Data {
    [CreateAssetMenu(fileName = "AmmoItemInfo", menuName = "PocketZoneTest/Info/Create New Ammo Info")]
    [Serializable]
    public class AmmoItemInfo : ScriptableObject {
        public ItemAmmoType AmmoType { get; }
        public float Damage => _damage;
        public Sprite BulletSprite => _bulletSprite;
        [SerializeField] ItemAmmoType _ammoType;
        [SerializeField] float _damage = 10f;
        [SerializeField] Sprite _bulletSprite;
    }
}