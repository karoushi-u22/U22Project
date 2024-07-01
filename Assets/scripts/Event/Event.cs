using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using UnityEngine;

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
                jsonFilePath = Path.Combine(Application.dataPath, eventFolderPath, jsonFilePath);
                Debug.Log("path: " + jsonFilePath);

                StreamReader reader = new(jsonFilePath);
                string jsonData = reader.ReadToEnd();
                reader.Close();

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
