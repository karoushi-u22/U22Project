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
        private EventSelection eventSelection;
        private EventSelection.PlayerSelection.Body.Selection selectChoice;
        private string jsonFileName;
        private bool startTextboxFlag = false;
        private bool onClickFlag = false;
        private bool onComplateSetTextFlag = false;
        private bool onClickButtonFlag = false;

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

            instance.eventSelection = JsonIoHandler.LoadSelecttionFromJson();

            // キャラクターコントローラーの取得
            characterController = GameObject.Find(characterGameobject).GetComponent<CharacterController2D>();

            // TextboxHandlerのイベントを使用
            TextboxHandler.StartTextboxEvent += StartTextbox;
        }

        void Start()
        {
            // TextboxHandlerのイベントを使用
            TextboxHandler.TextboxClickEvent += OnClickTextbox;
            TextboxHandler.CompleteSetTextEvent += OnCompleteSetText;
            SelectButtonHandler.ClickEvent += OnClickButton;
        }

        void OnDestroy()
        {
            // TextboxHandlerのイベントを解除
            TextboxHandler.StartTextboxEvent -= StartTextbox;
            TextboxHandler.TextboxClickEvent -= OnClickTextbox;
            TextboxHandler.CompleteSetTextEvent -= OnCompleteSetText;
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

            instance.jsonFileName = jsonFile;  // イベントのJsonファイル名を取得

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
                    onComplateSetTextFlag = false;

                    textboxHandler.SetTextMain(eventText.text, eventText.delay); // メインテキストを設定
                    textboxHandler.SetTextPlayerName(eventText.sender); // プレイヤー名を設定

                    if (eventText.selections != null)
                    {
                        yield return new WaitUntil(() => onComplateSetTextFlag == true);  // テキストが全て表示されるまで待機

                        SelectButtonHandler.ShowButton(eventText.selections);

                        yield return new WaitUntil(() => onClickButtonFlag == true);  // いずれかの選択肢のボタンが押されるまで待機

                        EventSelection.PlayerSelection playerSelection = new() { title = instance.jsonFileName + " : " + eventText.text };  // 選択肢を格納するインスタンスを新規作成

                        foreach (var selection in eventText.selections)  // 各選択肢を取り出し、選択肢の配列に追加
                        {
                            if (selectChoice.title.Equals(selection))  // プレイヤーが選んだ選択肢を追加
                            {
                                playerSelection.body.selections.Add(selectChoice);
                            }
                            else  // プレイヤーが選ばなかった選択肢を追加
                            {
                                playerSelection.body.selections.Add(new EventSelection.PlayerSelection.Body.Selection{
                                    title = selection,
                                    playerSelected = false
                                });
                            }
                        }

                        eventSelection.playerSelections.Add(playerSelection);  // 選択肢を格納したインスタンスを選択肢の配列に追加
                        JsonIoHandler.SaveSelectionsToJson(eventSelection);  // 選択肢をJsonに保存
                    }
                    else
                    {
                        yield return new WaitUntil(() => onClickFlag == true);
                    }
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

        // テキストが全て表示された時のイベント
        void OnCompleteSetText()
        {
            onComplateSetTextFlag = true;
        }

        // 選択肢のボタンが押された時のイベント
        void OnClickButton(EventSelection.PlayerSelection.Body.Selection selection)
        {
            onClickButtonFlag = true;
            instance.selectChoice = selection;
        }
    }
}
