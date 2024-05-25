using System;
using System.IO;
using System.Text.Json;
using UnityEngine;

namespace U22Game.Handlers
{
    public static class JsonIoHandler
    {
        public static readonly string saveFileName = "saveData.json";

        public static string GetSaveFilePath()
        {
            return Path.Combine(Application.persistentDataPath, saveFileName);
        }

        public static void SaveToJson(SaveDataHandler saveData)
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonData = JsonSerializer.Serialize(saveData, options);
                File.WriteAllText(GetSaveFilePath(), jsonData);
                Debug.Log("Save successful : " + GetSaveFilePath());
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to save data: {e}");
            }
        }

        public static SaveDataHandler LoadFromJson()
        {
            try
            {
                string jsonData = File.ReadAllText(GetSaveFilePath());
                SaveDataHandler saveData = JsonSerializer.Deserialize<SaveDataHandler>(jsonData);
                Debug.Log("Load successful : " + GetSaveFilePath());
                return saveData;
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to load data: {e}");
                return null;
            }
        }
    }
}
