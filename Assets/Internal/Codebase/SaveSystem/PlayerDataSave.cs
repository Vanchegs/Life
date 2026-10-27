using System;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

namespace Codebase
{
    public static class PlayerDataSave
    {
        private static readonly string savePath = Application.persistentDataPath + "/SavePlayerData.json";
        private static readonly string backupPath = savePath + ".bak";
        private static readonly string temporaryPath = savePath + ".tmp";

        public static bool Save<TData>(TData data)
        {
            if (data == null)
                return false;

#if !UNITY_EDITOR
            const Formatting formatting = Formatting.None;
#else
            const Formatting formatting = Formatting.Indented;
#endif
            try
            {
                var json = JsonConvert.SerializeObject(data, formatting);
                EnsureDirectoryExists();
                File.WriteAllText(temporaryPath, json);

                if (File.Exists(savePath))
                    File.Replace(temporaryPath, savePath, backupPath);
                else
                    File.Move(temporaryPath, savePath);

                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"Ошибка при сохранении данных: {e.Message}");
                return false;
            }
        }

        public static TData Load<TData>(TData defaultData = default)
        {
            if (TryLoad(savePath, out TData data))
                return data;

            if (TryLoad(backupPath, out data))
            {
                Debug.LogWarning("Сохранение загружено из резервной копии");
                return data;
            }

            return defaultData;
        }

        public static void ClearSaveFile()
        {
            try
            {
                File.Delete(savePath);
                File.Delete(backupPath);
                File.Delete(temporaryPath);
            }
            catch (Exception e)
            {
                Debug.LogError($"Ошибка при удалении сохранения: {e.Message}");
            }
        }

        private static bool TryLoad<TData>(string path, out TData data)
        {
            data = default;

            if (!File.Exists(path))
                return false;

            try
            {
                data = JsonConvert.DeserializeObject<TData>(File.ReadAllText(path));
                return data != null;
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Не удалось загрузить сохранение {path}: {e.Message}");
                return false;
            }
        }

        private static void EnsureDirectoryExists()
        {
            var directory = Path.GetDirectoryName(savePath);

            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                Directory.CreateDirectory(directory);
        }
    }
}
