using Assets.Scripts.InventoryObject.Abstract;
using Assets.Scripts.InventoryObject.Data;
using UnityEngine;

namespace Assets.Scripts.Infra.Game {
    public interface IGameInventoryItemsFactory {
        void CreateInventoryLootItem(object sender,Vector2 position, IInventoryItemInfo itemInfo, int amount = 1);
        void CreateBullet(object sender, Transform transform, IInventoryItemInfo  itemInfo, int amount =1 );
    }
}