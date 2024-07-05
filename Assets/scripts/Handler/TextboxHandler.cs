using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

namespace U22Game.Handlers
{
    public class TextboxHandler : MonoBehaviour
    {
        public static event UnityAction<TextboxHandler> StartTextboxEvent;
        public static event UnityAction TextboxClickEvent;
        public static event UnityAction CompleteSetTextEvent;
        private bool waitFlag = false;
        private Canvas textbox;
        private Coroutine setTextCoroutine;
        [SerializeField] private TextMeshProUGUI textfieldMain;
        [SerializeField] private TextMeshProUGUI textfieldPlayerName;
        [SerializeField] private float delayStart = 0.5f;  // 最初の文字を表示するまでの時間
        [SerializeField] private float delayDuration = 0.1f;  // 次の文字を表示するまでの時間

        private void Start()
        {
            // Canvasコンポーネントの取得
            if (textbox == null && !TryGetComponent<Canvas>(out textbox))
            {
                Debug.LogError("Canvas component not found.");
            }

            // TextMeshProUGUIコンポーネントの確認
            if (textfieldMain == null)
            {
                Debug.LogError("TextMeshProUGUI component for textfieldMain not assigned.");
            }

            if (textfieldPlayerName == null)
            {
                Debug.LogError("TextMeshProUGUI component for textfieldPlayerName not assigned.");
            }

            // 初期値はTextbox非表示
            HideTextbox();

            // Textbox読み込み完了時にイベントを発火
            StartTextboxEvent?.Invoke(this);
        }

        private void Update()
        {
            // Textbox表示されているとき
            if (textbox.enabled)
            {
                // マウスクリックまたはEnterキー押下を検出
                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return))
                {
                    int length = textfieldMain.text.Length;

                    // setTextCoroutineが終了したとき
                    if (!waitFlag && setTextCoroutine == null)
                    {
                        TextboxClickEvent?.Invoke();
                    }
                    // テキストが全て表示されていないとき、コルーチンを停止しテキストを全て表示する
                    else if (!waitFlag && textfieldMain.maxVisibleCharacters < length)
                    {
                        StopCoroutine(setTextCoroutine);
                        setTextCoroutine = null;
                        textfieldMain.maxVisibleCharacters = length;
                        CompleteSetTextEvent?.Invoke();
                    }
                }
            }
        }

        // 一文字ずつテキストをセットするコルーチン
        private IEnumerator SetTextCoroutine(TextMeshProUGUI textMeshPro, string newText, int delaySkip)
        {
            textMeshPro.maxVisibleCharacters = 0;

            if (textfieldMain != null)
            {
                textfieldMain.text = newText;
            }

            StartCoroutine(WaitSkipCoroutine(delaySkip));
            yield return new WaitForSeconds(delayStart);

            int length = textMeshPro.text.Length;
            for (int i = 0; i < length; i++)
            {
                textMeshPro.maxVisibleCharacters = i + 1;

                // 改行がある場合、Delayを多く取る
                if (textMeshPro.text[i] == '\n')
                {
                    yield return new WaitForSeconds(delayDuration * 10);
                }
                else
                {
                    yield return new WaitForSeconds(delayDuration);
                }
            }

            textMeshPro.maxVisibleCharacters = length;

            setTextCoroutine = null;

            CompleteSetTextEvent?.Invoke();
        }

        private IEnumerator WaitSkipCoroutine(int delaySkip)
        {
            waitFlag = true;

            Debug.Log($"Wait Time: {Math.Max(delayStart, delaySkip)}");
            yield return new WaitForSeconds(Math.Max(delayStart, delaySkip));

            Debug.Log("Skip Available");
            waitFlag = false;
        }

        // Textboxを表示するメソッド
        public void ShowTextbox()
        {
            if (textbox != null)
            {
                textbox.enabled = true;
            }
        }

        // Textboxを非表示にするメソッド
        public void HideTextbox()
        {
            if (textbox != null)
            {
                textbox.enabled = false;
            }
        }

        // TextMeshProの1つ目のテキストを変更するメソッド
        public void SetTextMain(string newText, int delaySkip)
        {
            if (setTextCoroutine != null)
            {
                StopCoroutine(setTextCoroutine);
            }

            setTextCoroutine = StartCoroutine(SetTextCoroutine(textfieldMain, newText, delaySkip));
        }

        // TextMeshProの2つ目のテキストを変更するメソッド
        public void SetTextPlayerName(string newText)
        {
            if (textfieldPlayerName != null)
            {
                textfieldPlayerName.text = newText;
            }
        }
    }
}
