using System;
using System.Collections.Generic;
using System.Text.Json;
using UnityEngine;

namespace U22Game.Events
{
    [Serializable]
    public class EventText
    {
        public string text { get; set; }            // テキスト
        public string sender { get; set; }          // 送信者
        public string highlight { get; set; }       // ハイライト対象のオブジェクトID
        public int delay { get; set; }              // テキスト送りが可能になるまでの秒数
        public string map_id { get; set; }          // マップID
        public CameraPosition camera_pos { get; set; }  // カメラ位置

        [Serializable]
        public class CameraPosition
        {
            public float X { get; set; }              // カメラ位置のx座標
            public float Y { get; set; }              // カメラ位置のy座標
        }

        // JSONからEventTextのリストを読み込む静的メソッド
        public static List<EventText> FromJson(string jsonString)
        {
            try
            {
                return JsonSerializer.Deserialize<List<EventText>>(jsonString);
            }
            catch (JsonException ex)
            {
                Debug.LogError($"Failed to deserialize JSON: {ex.Message}");
                return null;
            }
        }
    }

}
