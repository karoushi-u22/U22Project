using System;
using System.IO;
using System.Text.Json;
using U22Game.Events;
using UnityEngine;

namespace U22Game.Handlers
{
    public static class JsonIoHandler
    {
        public static readonly string saveFileName = "saveData.json";
        public static readonly string selectionFileName = "playerSelections.json";

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

        public static string GetSelectionFilePath()
        {
            return Path.Combine(Application.persistentDataPath, selectionFileName);
        }

        public static void SaveSelectionsToJson(EventSelection eventSelection)
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonData = JsonSerializer.Serialize(eventSelection, options);
                File.WriteAllText(GetSelectionFilePath(), jsonData);
                Debug.Log("Save selection successful : " + GetSelectionFilePath());
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to save selection: {e}");
            }
        }

        public static EventSelection LoadSelecttionFromJson()
        {
            try
            {
                string jsonData = File.ReadAllText(GetSelectionFilePath());
                EventSelection eventSelection = JsonSerializer.Deserialize<EventSelection>(jsonData);
                Debug.Log("Load successful : " + GetSaveFilePath());
                return eventSelection;
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to load data: {e}");
                return null;
            }
        }
    }
}
