using System.Collections;
using Assets.Scripts.Enemy.Abstract;
using Assets.Scripts.InventoryObject;
using Assets.Scripts.InventoryObject.Abstract;
using Assets.Scripts.InventoryObject.Data;
using Assets.Scripts.Player.Abstract;
using Pathfinding;
using UnityEngine;

namespace Assets.Scripts.Enemy.DataRes {
    // Ресурсы для создания противников
    [CreateAssetMenu(fileName = "EnemyResourceData", menuName = "PocketZoneTest/EnemyResourceData")]

    public class EnemyResourceData : ScriptableObject {

        public Sprite SpriteEnemy;

        public InventoryItemInfo LootInfo;

        public float MaxBaseHealth => _maxBaseHealth;

        public InventoryItemInfo Info => _itemWeaponInfo;
        [SerializeField] InventoryItemInfo _itemWeaponInfo;

        [SerializeField] float _maxBaseHealth = 100f;
    }
}