using System.Collections.Generic;
using Assets.Scripts.InventoryObject.Data;
using Assets.Scripts.Main.Game.Abstract;
using Assets.Scripts.Player.Abstract;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Main.Game {
    // Реализация правил игры на уровне
    public class GameMode : IGameMode {
        IGameController _c;
        int _collectedCoins = 0;
        SceneNames _currentScene;
        Dictionary<SceneNames, (InventoryItemType ItemType, int Count)> _taskTargets = new Dictionary<SceneNames, (InventoryItemType, int)> {
            { SceneNames.Game1, (InventoryItemType.Coin, 3) },
            { SceneNames.Game2, (InventoryItemType.Coin, 3) }
        };
        public GameMode(IGameController controller) {
            _c=controller;
        }

        public void ChangeState(SceneNames sceneName) {
            _currentScene = sceneName;
            if (_taskTargets.ContainsKey(_currentScene)) {
                
                _collectedCoins = 0; // Сбросить счетчик монет при смене сцены
                InitTaskPlayer(_c.RD.Player);
            } else {
                // Действия, если сцены нет в словаре (если требуется)
                Debug.LogWarning($"Scene {_currentScene} is not found in the task targets dictionary.");
            }
        }

        public void InitTaskPlayer(IPlayerController player) {
            if (_taskTargets.ContainsKey(_currentScene)) {
                player.UpdateTask(_collectedCoins, _taskTargets[_currentScene].Count,
                    _taskTargets[_currentScene].ItemType);
            }
        }

        public void CollectCoin(IPlayerController player) {
            _collectedCoins++;
            UpdateTaskPlayer(player);
            // Проверить, достигнуто ли задание для текущей сцены
            if (_collectedCoins >= _taskTargets[_currentScene].Count) {
                // Задание выполнено
                OnTaskCompleted();
                _c.RD.UIController.ShowPopup(UIPopupType.TaskComplete);
            }
        }

        private void UpdateTaskPlayer(IPlayerController player) {
            
                player.UpdateTask(_collectedCoins, _taskTargets[_currentScene].Count, _taskTargets[_currentScene].ItemType);
            
        }


        private void OnTaskCompleted() {
            // Логика при успешном выполнении задания
            Debug.Log($"Task for {_currentScene} completed!");
            _c.TaskCompleted();
        }
    }
}