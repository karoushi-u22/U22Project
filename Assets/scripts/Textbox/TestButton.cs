using UnityEngine;

namespace U22Game.Handlers {
    public class TestButton : MonoBehaviour {
        public TextboxHandler textboxHandler; // TextboxHandlerのインスタンスをUnityエディタで指定します。

        void Update() {
            // Oキーが押されたらTextNameを表示
            if (Input.GetKeyDown(KeyCode.O)) {
                if (textboxHandler != null) {
                    textboxHandler.ToggleTextName(true);
                } else {
                    Debug.LogError("TextboxHandlerが割り当てられていません。");
                }
            }

            // Pキーが押されたらTextNameを非表示
            if (Input.GetKeyDown(KeyCode.P)) {
                if (textboxHandler != null) {
                    textboxHandler.ToggleTextName(false);
                } else {
                    Debug.LogError("TextboxHandlerが割り当てられていません。");
                }
            }

            // Kキーが押されたらTextFieldを表示
            if (Input.GetKeyDown(KeyCode.K)) {
                if (textboxHandler != null) {
                    textboxHandler.ToggleTextField(true);
                } else {
                    Debug.LogError("TextboxHandlerが割り当てられていません。");
                }
            }

            // Lキーが押されたらTextFieldを非表示
            if (Input.GetKeyDown(KeyCode.L)) {
                if (textboxHandler != null) {
                    textboxHandler.ToggleTextField(false);
                } else {
                    Debug.LogError("TextboxHandlerが割り当てられていません。");
                }
            }

            // Mキーが押されたらTextFlameを表示
            if (Input.GetKeyDown(KeyCode.M)) {
                if (textboxHandler != null) {
                    textboxHandler.ToggleTextFlame(true);
                } else {
                    Debug.LogError("TextboxHandlerが割り当てられていません。");
                }
            }

            // Nキーが押されたらTextFlameを非表示
            if (Input.GetKeyDown(KeyCode.N)) {
                if (textboxHandler != null) {
                    textboxHandler.ToggleTextFlame(false);
                } else {
                    Debug.LogError("TextboxHandlerが割り当てられていません。");
                }
            }

            // テキストの変更テスト
            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                if (textboxHandler != null) {
                    textboxHandler.ChangeTextName("新しい名前");
                } else {
                    Debug.LogError("TextboxHandlerが割り当てられていません。");
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha2)) {
                if (textboxHandler != null) {
                    textboxHandler.ChangeTextField("新しいセリフ");
                } else {
                    Debug.LogError("TextboxHandlerが割り当てられていません。");
                }
            }
        }
    }
}
