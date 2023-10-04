using UnityEngine;

namespace Assets.Scripts.Infra.Game.Data {
    public interface IGameResourceData {
        public GameObject LootPrefab { get; }
        public IGameInventoryItemsFactory InventoryItemsFactory { get; }
    }
}