using Assets.Scripts.InventoryObject.Abstract;
using Assets.Scripts.InventoryObject.Data;
using UnityEngine;

namespace Assets.Scripts.Infra.Game {
    public interface IGameInventoryItemsFactory {
        void CreateInventoryItem(object sender,Vector2 position, IInventoryItemInfo itemInf, int amount);
    }
}