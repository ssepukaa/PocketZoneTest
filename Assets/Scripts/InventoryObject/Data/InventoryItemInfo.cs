using Assets.Scripts.InventoryObject.Abstract;
using UnityEngine;

namespace Assets.Scripts.InventoryObject.Data {

    public enum InventoryItemType { Empty, Ammo, Rifle, Pistol, Claws, Coin,}
    public enum ItemFunctionalityType{None, Ammo, Weapon, Currency, }
    public enum ItemIsEquippableType{NotEquippable,Equippable,}
    public enum ItemAmmoType {None, RifleAmmo, PistolAmmo, }
    public enum WeaponType{None, Melee, Range}

    [CreateAssetMenu(fileName = "InventoryItemInfo", menuName = "PocketZoneTest/Info/Create New Item Info")]
    public class InventoryItemInfo : ScriptableObject, IInventoryItemInfo {
        
        
        [SerializeField]private string _id;
        [SerializeField] private string _title;
        [SerializeField] private string _description;
        [SerializeField] private int _maxAmountSlot;
        [SerializeField] private Sprite _spriteIcon;
        [SerializeField] private bool _isEquip;
        [SerializeField] public InventoryItemType _itemType;
        [SerializeField] private ItemFunctionalityType _functionalityType;
        [SerializeField] private ItemIsEquippableType _itemEquippableType;
        
       
        [SerializeField] private  WeaponItemInfo _weaponWeaponInfo;
        [SerializeField] private AmmoItemInfo _ammoItemInfo;
        public string Id => _id;
        public string Title => _title;
        public string Description => _description;
        public int MaxAmountSlot => _maxAmountSlot;
        public Sprite SpriteIcon => _spriteIcon;
        public bool IsEquip => _isEquip;

       

        public InventoryItemType ItemType => _itemType;
        public ItemFunctionalityType FunctionalityType => _functionalityType;
        public ItemIsEquippableType ItemEquippableType => _itemEquippableType;
      

        public WeaponItemInfo WeaponInfo => _weaponWeaponInfo;

        public AmmoItemInfo AmmoInfo => _ammoItemInfo;
    }
}