using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Assets.Scripts.Infra.Boot {
    public class BinarySerializationHelper {
        static string defaultSavePath = Path.Combine(Application.persistentDataPath, "saveData.bin");

        // Сериализация объекта в бинарный файл
        public static void SerializeToFile<T>(T obj, string filePath = null) {
            if (filePath == null) {
                filePath = defaultSavePath;
            }

            try {
                using (FileStream fs = new FileStream(filePath, FileMode.Create)) {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(fs, obj);
                }
            } catch (Exception e) {
                Debug.LogError($"Failed to serialize data to file {filePath}. Error: {e.Message}");
            }
        }

        // Десериализация объекта из бинарного файла
        public static T DeserializeFromFile<T>(string filePath = null) {
            if (filePath == null) {
                filePath = defaultSavePath;
            }

            if (!File.Exists(filePath)) {
                Debug.LogWarning($"File {filePath} does not exist. Returning default value.");
                return default(T);
            }

            try {
                using (FileStream fs = new FileStream(filePath, FileMode.Open)) {
                    BinaryFormatter formatter = new BinaryFormatter();
                    return (T)formatter.Deserialize(fs);
                }
            } catch (Exception e) {
                Debug.LogError($"Failed to deserialize data from file {filePath}. Error: {e.Message}");
                return default(T);
            }
        }
    }
}