using UnityEngine;

namespace Assets.Scripts.InventoryObject.Data {
   
    

    [CreateAssetMenu(fileName = "ItemsInfoDataBase", menuName = "PocketZoneTest/Items/Create Data Base Info")]
    public class ItemsInfoDataBase : ScriptableObject {
        // public List<InventoryItemInfo> itemTypeToInfo = new List<InventoryItemInfo>();
        public InventoryItemInfo AmmoInfo;
        public InventoryItemInfo RifleInfo;
        public InventoryItemInfo PistolInfo;

    }
}