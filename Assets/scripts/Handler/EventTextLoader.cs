using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using U22Game.Events;
using U22Game.Controller;

namespace U22Game.Handlers
{
    public class EventTextLoader : MonoBehaviour
    {
        private static EventTextLoader instance;
        private static CharacterController2D characterController;
        private readonly string characterGameobject = "main_character_1";
        private TextboxHandler textboxHandler;
        private bool startTextboxFlag = false;
        private bool onClickFlag = false;

        void Awake()
        {
            // インスタンスを保持
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            // キャラクターコントローラーの取得
            characterController = GameObject.Find(characterGameobject).GetComponent<CharacterController2D>();
        }

        void Start()
        {
            // TextboxHandlerのイベントを使用
            TextboxHandler.StartTextboxEvent += StartTextbox;
            TextboxHandler.TextboxClickEvent += OnClickTextbox;
        }

        void OnDestroy()
        {
            // TextboxHandlerのイベントを解除
            TextboxHandler.StartTextboxEvent -= StartTextbox;
            TextboxHandler.TextboxClickEvent -= OnClickTextbox;
        }

        static public void LoadEvent(string jsonFile)
        {
            // JSONファイルの読み込み
            List<EventText> eventTextList = EventText.FromJson(jsonFile);

            // 読み込んだデータをログ表示
            foreach (var eventText in eventTextList)
            {
                Debug.Log($"Sender: {eventText.sender}, Text: {eventText.text}");
            }

            // イベントテキストを順に表示
            instance.StartCoroutine(instance.DisplayEventText(eventTextList));
        }

        private IEnumerator DisplayEventText(List<EventText> eventTextList)
        {
            // textboxHandlerインスタンス取得するまで待機
            yield return new WaitUntil(() => startTextboxFlag == true);

            // イベント実行中はキャラクターの操作を受け付けない
            characterController.enabled = false;

            if (textboxHandler != null)
            {
                textboxHandler.ShowTextbox(); // テキストボックスを表示

                foreach (var eventText in eventTextList)
                {
                    onClickFlag = false;

                    textboxHandler.SetTextMain(eventText.text); // メインテキストを設定
                    textboxHandler.SetTextPlayerName(eventText.sender); // プレイヤー名を設定

                    yield return new WaitUntil(() => onClickFlag == true);
                }

                textboxHandler.HideTextbox(); // テキストボックスを非表示
            }
            else
            {
                Debug.LogError("TextboxHandler could not be obtained.");
            }

            characterController.enabled = true;
        }

        void StartTextbox(TextboxHandler textbox)
        {
            // TextboxHandlerの取得
            textboxHandler = textbox;
            startTextboxFlag = true;
        }

        void OnClickTextbox()
        {
            onClickFlag = true;
        }
    }
}
