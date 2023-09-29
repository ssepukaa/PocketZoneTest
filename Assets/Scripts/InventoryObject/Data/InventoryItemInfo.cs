using System;
using System.Collections.Generic;
using Assets.Scripts.InventoryObject.Items;
using UnityEngine;

namespace Assets.Scripts.InventoryObject.Data {

    public enum InventoryItemType { Apple, Pepper, }

    [CreateAssetMenu(fileName = "InventoryItemInfo", menuName = "PocketZoneTest/Items/Create New Item Info")]
    public class InventoryItemInfo : ScriptableObject {
        public static Dictionary<InventoryItemType, Type> itemTypeToClassMap = new Dictionary<InventoryItemType, Type> {
        { InventoryItemType.Apple, typeof(Apple) },
        { InventoryItemType.Pepper, typeof(Pepper) },
        // ... другие типы предметов
        };
        public InventoryItemType inventoryItemType;
        public string id;
        public string title;
        public string description;
        public int maxAmountSlot;
        public Sprite spriteIcon;
    }
}