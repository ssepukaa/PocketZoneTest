using System.IO;
using Assets.Scripts.InventoryObject;
using Assets.Scripts.InventoryObject.Abstract;
using Assets.Scripts.InventoryObject.Data;
using Assets.Scripts.Player.Data;
using UnityEngine;

namespace Assets.Scripts.Infra.Game.SaveLoad {
    public class SaveLoadManager {

        private static string savePath = Application.persistentDataPath + "/playerSaveData.json";

        public static void SavePlayer(PlayerModelData playerModel, ItemsInfoDataBase itemDatabase) {
            PlayerSaveData saveData = new PlayerSaveData();

            // Преобразование данных из PlayerModelData в PlayerSaveData
            saveData.IsCompleteGame1 = playerModel.IsCompleteGame1;
            saveData.IsCompleteGame2 = playerModel.IsCompleteGame2;

            // Сохранение массива InventorySlots
            saveData.InventorySlots = new InventoryItemSaveData[playerModel._slots.Length];
            for (int i = 0; i < playerModel._slots.Length; i++) {
                saveData.InventorySlots[i] = new InventoryItemSaveData();
                if (playerModel._slots[i]?.Item != null) {
                    saveData.InventorySlots[i].ItemType = playerModel._slots[i].Item.ItemType;
                    saveData.InventorySlots[i].Amount = playerModel._slots[i].Item.Amount;
                }
            }

            saveData.ClipSlot = new InventoryItemSaveData();
            if (playerModel._clipSlot?.Item != null) {
                saveData.ClipSlot.ItemType = playerModel._clipSlot.Item.ItemType;
                saveData.ClipSlot.Amount = playerModel._clipSlot.Item.Amount;
            }

            saveData.WeaponSlot = new InventoryItemSaveData();
            if (playerModel._weaponSlot?.Item != null) {
                saveData.WeaponSlot.ItemType = playerModel._weaponSlot.Item.ItemType;
                saveData.WeaponSlot.Amount = playerModel._weaponSlot.Item.Amount;
            }

            saveData.InventoryCapacity = playerModel.InventoryCapacity;

            string json = JsonUtility.ToJson(saveData);
            File.WriteAllText(savePath, json);
            Debug.Log("Данные игрока успешно сохранены.");
        }

        public static PlayerModelData LoadPlayer(ItemsInfoDataBase itemDatabase) {
            if (File.Exists(savePath)) {
                string json = File.ReadAllText(savePath);
                PlayerSaveData saveData = JsonUtility.FromJson<PlayerSaveData>(json);

                PlayerModelData playerModel = new PlayerModelData();

                // Преобразование данных из PlayerSaveData в PlayerModelData
                playerModel.IsCompleteGame1 = saveData.IsCompleteGame1;
                playerModel.IsCompleteGame2 = saveData.IsCompleteGame2;

                // Загрузка массива InventorySlots
                playerModel._slots = new IInventorySlot[saveData.InventorySlots.Length];
                for (int i = 0; i < saveData.InventorySlots.Length; i++) {
                    playerModel._slots[i] = new InventorySlot(); // или другой подходящий конструктор
                    if (itemDatabase.itemTypetMap.ContainsKey(saveData.InventorySlots[i].ItemType)) {
                        playerModel._slots[i].Item = new InventoryItem(itemDatabase.itemTypetMap[saveData.InventorySlots[i].ItemType]);
                        playerModel._slots[i].Item.Amount = saveData.InventorySlots[i].Amount;
                    }
                }

                // Загрузка ClipSlot и WeaponSlot
                playerModel._clipSlot = new InventorySlot();
                if (itemDatabase.itemTypetMap.ContainsKey(saveData.ClipSlot.ItemType)) {
                    playerModel._clipSlot.Item = new InventoryItem(itemDatabase.itemTypetMap[saveData.ClipSlot.ItemType]);
                    playerModel._clipSlot.Item.Amount = saveData.ClipSlot.Amount;
                }

                playerModel._weaponSlot = new InventorySlot();
                if (itemDatabase.itemTypetMap.ContainsKey(saveData.WeaponSlot.ItemType)) {
                    playerModel._weaponSlot.Item = new InventoryItem(itemDatabase.itemTypetMap[saveData.WeaponSlot.ItemType]);
                    playerModel._weaponSlot.Item.Amount = saveData.WeaponSlot.Amount;
                }

                playerModel.InventoryCapacity = saveData.InventoryCapacity;
                playerModel._inventory = new Inventory(playerModel.InventoryCapacity, playerModel, false);

                Debug.Log("Данные игрока успешно загружены.");
                // Инициализация инвентаря на основе загруженных данных
               

                return playerModel;
            }
            Debug.LogWarning("Файл сохранения не найден.");
            return null; // Возвращает null, если файл не существует
        }
    }
}