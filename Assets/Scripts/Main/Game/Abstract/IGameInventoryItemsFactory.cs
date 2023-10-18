using Assets.Scripts.InventoryObject.Abstract;
using UnityEngine;

namespace Assets.Scripts.Main.Game.Abstract {
    // Фабрика лута и пуль
    public interface IGameInventoryItemsFactory {
        void CreateInventoryLootItem(object sender,Vector2 position, IInventoryItemInfo itemInfo, int amount = 1);
        void CreateBullet(object sender, Transform transform, IInventoryItemInfo  itemInfo, int amount =1 );
    }
}