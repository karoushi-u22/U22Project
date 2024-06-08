using UnityEngine;
using TMPro;

namespace U22Game.Handlers {
    public class TextboxHandler : MonoBehaviour {
        public TextMeshProUGUI textName;  // 現在セリフを表示させている人物名
        public TextMeshProUGUI textField; // セリフの内容
        public GameObject textFlame; // テキストフレームの画像オブジェクト

        // 表示されるテキスト制御のメソッド
        public void DisplayText(string characterName, string dialogue) {
            textName.text = characterName;
            textField.text = dialogue;
        }

        // TextNameの文字を変更するメソッド
        public void ChangeTextName(string newText) {
            textName.text = newText;
        }

        // TextFieldの文字を変更するメソッド
        public void ChangeTextField(string newText) {
            textField.text = newText;
        }

        // textNameの表示・非表示を切り替えるメソッド
        public void ToggleTextName(bool isVisible) {
            if (textName != null) {
                CanvasRenderer nameRenderer = textName.GetComponent<CanvasRenderer>();
                if (nameRenderer != null) {
                    nameRenderer.SetAlpha(isVisible ? 1.0f : 0.0f);
                }
            } else {
                Debug.LogError("textNameが割り当てられていません。");
            }
        }

        // textFieldの表示・非表示を切り替えるメソッド
        public void ToggleTextField(bool isVisible) {
            if (textField != null) {
                CanvasRenderer fieldRenderer = textField.GetComponent<CanvasRenderer>();
                if (fieldRenderer != null) {
                    fieldRenderer.SetAlpha(isVisible ? 1.0f : 0.0f);
                }
            } else {
                Debug.LogError("textFieldが割り当てられていません。");
            }
        }

        // textFlameの表示・非表示を切り替えるメソッド
        public void ToggleTextFlame(bool isVisible) {
            if (textFlame != null) {
                CanvasRenderer flameRenderer = textFlame.GetComponent<CanvasRenderer>();
                if (flameRenderer != null) {
                    flameRenderer.SetAlpha(isVisible ? 1.0f : 0.0f);
                }
            } else {
                Debug.LogError("textFlameが割り当てられていません。");
            }
        }
    }
}
