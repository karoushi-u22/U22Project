using UnityEngine;
using UnityEngine.UI;

namespace U22Game.Handlers
{
    public class ButtonVisibilityHandler : MonoBehaviour
    {
        private SaveDataHandler saveData; // SaveDataHandlerのインスタンス
        private Button targetButton; // 非表示にする対象のボタン
        [SerializeField] private Button targetMoveButton; // 非表示時に移動するボタン
        [SerializeField] private Vector2 newPosition; // 非表示時に移動する位置

        void Start()
        {
            // このスクリプトがアタッチされているボタンを取得
            targetButton = GetComponent<Button>();

            // ボタンの表示・非表示をチェックする
            CheckAndSetButtonVisibility();
        }

        void CheckAndSetButtonVisibility()
        {
            saveData = JsonIoHandler.LoadFromJson();

            // SaveDatasの項目数を取得
            int saveDataCount = saveData.SaveDatas.Count;

            // CurrentDateを取得
            int currentDate = saveData.CurrentDate;

            // 項目数とCurrentDateが一致した場合にボタンを非表示にする
            if (saveDataCount == currentDate)
            {
                targetButton.gameObject.SetActive(false);

                // RectTransformを使用して位置を変更
                RectTransform rectTransform = targetMoveButton.GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    rectTransform.anchoredPosition = newPosition;
                }
            }
            else
            {
                targetButton.gameObject.SetActive(true);
            }
        }
    }
}
