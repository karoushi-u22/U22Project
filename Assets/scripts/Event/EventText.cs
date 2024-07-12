using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using UnityEngine;

namespace U22Game.Events
{
    [Serializable]
    public class EventText
    {
        private static readonly string eventFolderPath = "StoryEvents";  // イベントファイルが入っているフォルダパス(Assetsフォルダ以降のパス)
        public string text { get; set; }            // テキスト
        public string sender { get; set; }          // 送信者
        public string highlight { get; set; }       // ハイライト対象のオブジェクトID
        public int delay { get; set; }              // テキスト送りが可能になるまでの秒数
        public string map_id { get; set; }          // マップID
        public List<string> selections{ get; set; }  // 選択肢の配列
        public CameraPosition camera_pos { get; set; }  // カメラ位置

        [Serializable]
        public class CameraPosition
        {
            public float X { get; set; }              // カメラ位置のx座標
            public float Y { get; set; }              // カメラ位置のy座標
        }

        // JSONからEventTextのリストを読み込む静的メソッド
        public static List<EventText> FromJson(string jsonFilePath)
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
                return JsonSerializer.Deserialize<List<EventText>>(jsonData);
            }
            catch (JsonException ex)
            {
                Debug.LogError($"Failed to deserialize JSON: {ex.Message}");
                return null;
            }
        }
    }

}
