using UnityEngine;
//表示非表示 Debug用スクリプト
namespace U22Game.Handlers {
    public class TestButton : MonoBehaviour {
        public TextboxHandler textboxHandler; // TextboxHandlerのインスタンスをUnityエディタで指定します。

        void Update() {
            // Oキーが押されたらテキストボックスを表示
            if (Input.GetKeyDown(KeyCode.O)) {
                if (textboxHandler != null) {
                    textboxHandler.ToggleTextbox(true);
                } else {
                    Debug.LogError("TextboxHandlerが割り当てられていません。");
                }
            }

            // Pキーが押されたらテキストボックスを非表示
            if (Input.GetKeyDown(KeyCode.P)) {
                if (textboxHandler != null) {
                    textboxHandler.ToggleTextbox(false);
                } else {
                    Debug.LogError("TextboxHandlerが割り当てられていません。");
                }
            }
        }
    }
}
