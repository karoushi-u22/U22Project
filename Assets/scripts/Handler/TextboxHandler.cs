using UnityEngine;
using TMPro;

namespace U22Game.Handlers
{
    public class TextboxHandler : MonoBehaviour
    {
        private Canvas textbox;
        [SerializeField] private TextMeshProUGUI textfieldMain;
        [SerializeField] private TextMeshProUGUI textfieldPlayerName;

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
            if (textfieldMain != null)
            {
                textfieldMain.text = newText;
            }
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
