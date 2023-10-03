using System;
using System.Collections.Generic;
using Assets.Scripts.InventoryObject.Abstract;
using Assets.Scripts.InventoryObject.Items;
using UnityEngine;

namespace Assets.Scripts.InventoryObject.Data {

    

    [CreateAssetMenu(fileName = "InventoryItemInfo", menuName = "PocketZoneTest/Items/Create New Item Info")]
    public class InventoryItemInfo : ScriptableObject, IInventoryItemInfo {
        
        
        [SerializeField]private string _id;
        [SerializeField] private string _title;
        [SerializeField] private string _description;
        [SerializeField] private int _maxAmountSlot;
        [SerializeField] private Sprite _spriteIcon;
        [SerializeField] public InventoryItemType _itemType;
        public InventoryItemType ItemType => _itemType;
        public string Id => _id;
        public string Title => _title;
        public string Description => _description;
        public int MaxAmountSlot => _maxAmountSlot;
        public Sprite SpriteIcon => _spriteIcon;
    }
}