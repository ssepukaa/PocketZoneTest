using System.Collections.Generic;
using Assets.Scripts.Enemy.Abstract;
using Assets.Scripts.Main.Game.Abstract;
using Pathfinding;
using UnityEngine;

namespace Assets.Scripts.Main.Game {
    // Спавнер врагов
    public class EnemySpawner {
        IGameController _game;

        GridGraph _grid;

        public EnemySpawner(IGameController game) {
            _game = game;
            Construct();
        }
        public void Construct() {

            _grid = AstarPath.active.data.gridGraph;
            _game.RD.Enemies.Clear();
            for (int i = 0; i < _game.RD.NumberOfEnemies; i++) {
                Debug.Log($"Create enemy in cicle number {i}");
                SpawnEnemyAtRandomPosition();
            }


          

        }

        private void SpawnEnemyAtRandomPosition() {
            Vector3 randomPosition = GetRandomPositionOnGrid();
            _game.RD.Enemies = new List<IEnemyController>();

            if (randomPosition != Vector3.zero) // Если позиция найдена
            {
                var enemy = Object.Instantiate(_game.RD.EnemyPrefab, randomPosition, Quaternion.identity);
                var enemyController = enemy.GetComponent<IEnemyController>();
                enemyController.Construct(_game);

            }
        }

        private Vector3 GetRandomPositionOnGrid() {
            for (int i = 0; i < 300; i++) // Попробуйте найти позицию 30 раз
            {
                Vector3 randomPoint = _game.ControllerTransform.position + Random.insideUnitSphere * _game.RD.SpawnRadius;
                GraphNode node = _grid.GetNearest(randomPoint).node;

                if (node.Walkable) {
                    return (Vector3)node.position;
                }
            }

            return Vector3.zero; // Вернуть Vector3.zero, если подходящая позиция не найдена
        }
    }
}
