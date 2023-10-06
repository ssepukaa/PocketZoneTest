using UnityEngine;

namespace Assets.Scripts.InventoryObject.Data {
    [CreateAssetMenu(fileName = "WeaponItemInfo", menuName = "PocketZoneTest/ItemInfo/Create New Weapon Info")]
    public class WeaponItemInfo : ScriptableObject {
        [SerializeField] private ItemAmmoType _ammoType;
        [SerializeField] private float _fireRate = -1f;
        [SerializeField] private int _capacityClip = 30;
        public float Damage => _fireRate;
        public int CapacityClip => _capacityClip;
        public ItemAmmoType AmmoType { get; }
    
    }
}