﻿using Assets.Scripts.InventoryObject;
using Assets.Scripts.InventoryObject.Abstract;
using Assets.Scripts.InventoryObject.Data;
using Assets.Scripts.Player;
using Assets.Scripts.Player.Abstract;
using UnityEngine;

namespace Assets.Scripts.Infra.Game {
    public class GameInventoryItemsFactory: IGameInventoryItemsFactory {
        private IGameController _c;
        private const float RADIUS = 2.5f;
        private const int MAX_TRIES = 100;
        public GameInventoryItemsFactory(IGameController controller) {
            _c = controller;
        }

        public void CreateInventoryItem(object sender, Vector2 position, IInventoryItemInfo info, int amount) {
            IPlayerController playerController = sender as IPlayerController;
            GameObject Item = null;
            
            if (playerController != null) {
                Item = Object.Instantiate(_c.RD.LootPrefab, FindSpawnPosition(position),Quaternion.identity);
                
            }
            else {
                Item = Object.Instantiate(_c.RD.LootPrefab, position, Quaternion.identity);
            }

            var lootCont = Item.GetComponent<LootContainer>();
            if (lootCont != null) {
                lootCont.Construct(info, amount);
            }
           
        }

       


        private Vector2 FindSpawnPosition(Vector2 center) {
            for (int i = 0; i < MAX_TRIES; i++) {
                Vector2 randomDirection = Random.insideUnitCircle.normalized;
                Vector2 randomPosition = center + randomDirection * RADIUS;

                // Check if the position is free
                if (!PositionOccupied(randomPosition)) {
                    return randomPosition;
                }
            }
            return Vector2.zero;
        }

        private bool PositionOccupied(Vector2 position) {
            Collider2D hitCollider = Physics2D.OverlapCircle(position, 0.5f); // 0.5f is half the size of the item, adjust as needed
            return hitCollider != null;
        }

    }
}
