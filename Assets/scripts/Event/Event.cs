using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using UnityEngine;
using UnityEditor;

namespace U22Game.Events
{
    [Serializable]
    public class Event
    {
        private static readonly string eventFolderPath = "StoryEvents";  // イベントファイルが入っているフォルダパス(Assetsフォルダ以降のパス)
        public string eventFile { get; set; }            // イベントファイル
        public int executeDate { get; set; }              // イベント実行日
        public string executeTiming { get; set; }       // イベント実行タイミング

        // JSONからEventTextのリストを読み込む静的メソッド
        public static List<Event> FromJson(string jsonFilePath)
        {
            try
            {
                jsonFilePath = Path.Combine(eventFolderPath, jsonFilePath);
                TextAsset jsonTextAsset = Resources.Load<TextAsset>(jsonFilePath);

                if (jsonTextAsset == null)
                {
                    Debug.LogError($"Failed to load JSON file from Resources: {jsonFilePath}");
                    return null;
                }

                Debug.Log("path: " + jsonFilePath);

                string jsonData = jsonTextAsset.text;
                return JsonSerializer.Deserialize<List<Event>>(jsonData);
            }
            catch (JsonException ex)
            {
                Debug.LogError($"Failed to deserialize JSON: {ex.Message}");
                return null;
            }
        }
    }

}
