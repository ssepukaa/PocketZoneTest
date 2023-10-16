using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.InventoryObject.Data {
    
    [CreateAssetMenu(fileName = "ItemsInfoDataBase", menuName = "PocketZoneTest/Items/Create Data Base Info")]
    [Serializable]
    public class ItemsInfoDataBase : ScriptableObject {
        // public List<InventoryItemInfo> itemTypeToInfo = new List<InventoryItemInfo>();
        public InventoryItemInfo AmmoInfo;
        public InventoryItemInfo ClawsInfo;
        public InventoryItemInfo CoinInfo;
        public InventoryItemInfo PistolInfo;
        public InventoryItemInfo RifleInfo;

        public Dictionary<InventoryItemType, InventoryItemInfo> itemTypetMap;

        private void OnEnable() {
            InitializeDictionary();
        }

        private void InitializeDictionary() {
            itemTypetMap = new Dictionary<InventoryItemType, InventoryItemInfo> {
                { InventoryItemType.Ammo, AmmoInfo},
                { InventoryItemType.Claws , ClawsInfo},
                { InventoryItemType.Coin , CoinInfo},
                { InventoryItemType.Pistol , PistolInfo},
                { InventoryItemType.Rifle , RifleInfo},
            };
        }
    }
}