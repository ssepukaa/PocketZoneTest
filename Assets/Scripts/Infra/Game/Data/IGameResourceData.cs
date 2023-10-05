using Assets.Scripts.Player.Abstract;
using UnityEngine;

namespace Assets.Scripts.Infra.Game.Data {
    public interface IGameResourceData {
        public GameObject LootPrefab { get; }
        public IGameInventoryItemsFactory InventoryItemsFactory { get; }
        GameObject BulletPrefab { get; }
        IPlayerController Player { get; }
    }
}