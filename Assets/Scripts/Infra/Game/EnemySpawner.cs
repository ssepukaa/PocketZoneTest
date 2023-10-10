using Assets.Scripts.Enemy.Abstract;
using Assets.Scripts.Infra.Game.Abstract;
using Pathfinding;
using UnityEngine;

namespace Assets.Scripts.Infra.Game {
    public class EnemySpawner {
        private IGameController _game;
        
        private GridGraph _grid;

        public EnemySpawner(IGameController game) {
            _game = game;
            Construct();
        }
        public  void Construct() {
            _grid = AstarPath.active.data.gridGraph;

            for (int i = 0; i < _game.RD.NumberOfEnemies; i++) {
                SpawnEnemyAtRandomPosition();
            }
        }

        private void SpawnEnemyAtRandomPosition() {
            Vector3 randomPosition = GetRandomPositionOnGrid();

            if (randomPosition != Vector3.zero) // Если позиция найдена
            {
                var enemy = Object.Instantiate(_game.RD.EnemyPrefab, randomPosition, Quaternion.identity);
                _game.RD.Enemies.Add(enemy.GetComponent<IEnemyController>());
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
