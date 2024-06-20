using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

namespace U22Game.Handlers
{
    public class TextboxHandler : MonoBehaviour
    {
        public static event UnityAction<TextboxHandler> StartTextboxEvent;
        public static event UnityAction<TextboxHandler> TextboxClickEvent;
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
                    // setTextCoroutineが終了したとき
                    if (setTextCoroutine == null)
                    {
                        TextboxClickEvent?.Invoke(this);
                    }
                }
            }
        }

        // 一文字ずつテキストをセットするコルーチン
        private IEnumerator SetTextCoroutine(TextMeshProUGUI textMeshPro, string newText)
        {
            textMeshPro.maxVisibleCharacters = 0;

            yield return new WaitForSeconds(delayStart);

            if (textfieldMain != null)
            {
                textfieldMain.text = newText;
            }

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
        public void SetTextMain(string newText)
        {
            if (setTextCoroutine != null)
            {
                StopCoroutine(setTextCoroutine);
            }

            setTextCoroutine = StartCoroutine(SetTextCoroutine(textfieldMain, newText));
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
