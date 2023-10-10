using UnityEngine;

namespace Assets.Scripts.InventoryObject.Data {
    [CreateAssetMenu(fileName = "AmmoItemInfo", menuName = "PocketZoneTest/Info/Create New Ammo Info")]
    public class AmmoItemInfo : ScriptableObject {
        public ItemAmmoType AmmoType { get; }
        [SerializeField] private ItemAmmoType _ammoType;
        [SerializeField] private float _damage = 10f;
        [SerializeField] private Sprite _bulletSprite;
        public float Damage =>  _damage ;
        public Sprite BulletSprite => _bulletSprite;
    }
}