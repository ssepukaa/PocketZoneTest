using Assets.Scripts.InventoryObject.Abstract;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using UnityEngine;

namespace Assets.Scripts.InventoryObject.Data {
    [Serializable]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum InventoryItemType { Empty, Ammo, Rifle, Pistol, Claws, Coin,}
     
    public enum ItemFunctionalityType{None, Ammo, Weapon, Currency, }
    
    public enum ItemIsEquippableType{NotEquippable,Equippable,}
   
    public enum ItemAmmoType {None, RifleAmmo, PistolAmmo, }
   
    public enum WeaponType{None, Melee, Range}

    [CreateAssetMenu(fileName = "InventoryItemInfo", menuName = "PocketZoneTest/Info/Create New Item Info")]
    
    public class InventoryItemInfo : ScriptableObject, IInventoryItemInfo {
        public InventoryItemType ItemType => _itemType;
        public ItemFunctionalityType FunctionalityType => _functionalityType;
        public ItemIsEquippableType ItemEquippableType => _itemEquippableType;
        public string Id => _id;
        public string Title => _title;
        public string Description => _description;
        public int MaxAmountSlot => _maxAmountSlot;
        public bool IsEquip => _isEquip;
        public Sprite SpriteIcon => _spriteIcon;
        
        public WeaponItemInfo WeaponInfo => _weaponWeaponInfo;
        public AmmoItemInfo AmmoInfo => _ammoItemInfo;

        [SerializeField] ItemFunctionalityType _functionalityType;
        [SerializeField] ItemIsEquippableType _itemEquippableType;
        [SerializeField] string _id;
        [SerializeField] string _title;
        [SerializeField] string _description;
        [SerializeField] int _maxAmountSlot;
        [SerializeField] bool _isEquip;
        [SerializeField] public InventoryItemType _itemType;


        [SerializeField] Sprite _spriteIcon;
        [SerializeField]  WeaponItemInfo _weaponWeaponInfo;
        [SerializeField] AmmoItemInfo _ammoItemInfo;
    }
}