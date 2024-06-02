using UnityEngine;
using TMPro;

namespace U22Game.Handlers {
    public class TextboxHandler : MonoBehaviour {
        public TextMeshProUGUI textField; // TextMeshProのUI Textコンポーネントを使用

        // 表示されるテキスト制御のメソッド
        public void DisplayText(string newText) {
            // UI Textのテキストを新しいテキストに設定
            textField.text = newText;
        }

        // テキストを変更するメソッド
        public void ChangeText(string newText) {
            DisplayText(newText);
        }

        // テキストボックスの表示・非表示を切り替えるメソッド
        public void ToggleTextbox(bool isVisible) {
            if (textField != null) {
                // 透明度を設定して表示・非表示を切り替える
                CanvasRenderer canvasRenderer = textField.GetComponent<CanvasRenderer>();
                if (canvasRenderer != null) {
                    canvasRenderer.SetAlpha(isVisible ? 1.0f : 0.0f);
                }
            } else {
                Debug.LogError("textFieldが割り当てられていません。");
            }
        }
    }
}
