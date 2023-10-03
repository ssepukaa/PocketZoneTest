using Assets.Scripts.InventoryObject.Data;
using UnityEngine;

namespace Assets.Scripts.Infra.Game {
    public interface IGameInventoryItemsFactory {
        GameObject CreateInventoryItem(object sender,Vector2 position, InventoryItemType itemTypem, int amount);
    }
}