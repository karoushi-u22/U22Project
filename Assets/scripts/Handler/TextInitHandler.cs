using UnityEngine;
using TMPro;

namespace U22Game.Handlers
{
    public class TextInitHandler : MonoBehaviour
    {
        // TextMeshProUGUI コンポーネントの参照
        private TextMeshProUGUI textMeshPro;

        void Awake()
        {
            // 同じゲームオブジェクトにアタッチされている TextMeshProUGUI コンポーネントを取得
            textMeshPro = GetComponent<TextMeshProUGUI>();

            // TextMeshPro コンポーネントが存在しない場合のエラーハンドリング
            if (textMeshPro == null)
            {
                Debug.LogError("TextMeshProUGUI component not found on this game object.");
            }
        }

        // 外部からテキストを設定するためのパブリックメソッド
        public void SetText(string newText)
        {
            if (textMeshPro != null)
            {
                textMeshPro.text = newText;
            }
        }
    }
}
