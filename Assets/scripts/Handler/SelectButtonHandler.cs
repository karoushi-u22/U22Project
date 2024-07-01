using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using U22Game.Events;

namespace U22Game.Handlers
{
    public class SelectButtonHandler : MonoBehaviour
    {
        public static event UnityAction<EventSelection.PlayerSelection.Body.Selection> ClickEvent;
        public static SelectButtonHandler instance;
        private Transform buttonContainer; // ボタンを配置する親オブジェクト
        private Canvas canvas;  // ボタンを配置する親キャンバス
        [SerializeField] private GameObject buttonPrefab;  // ボタンのプレハブ
        [SerializeField] private float startYPosition = 150f;  // ボタンを配置する中央位置の垂直座標
        [SerializeField] private float yOffset = -150f; // ボタンの垂直オフセット


        void Awake()
        {
            if (canvas == null && !TryGetComponent<Canvas>(out canvas))
            {
                Debug.LogError("Canvas component not found.");
            }

            if (buttonContainer == null && !TryGetComponent<Transform>(out buttonContainer))
            {
                Debug.LogError("Transform component not found.");
            }

            // インスタンスを保持
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {
            ClearButtons();
            HideButton();
        }

        void GenerateButtons(List<string> selections)
        {
            float totalHeight = selections.Count * Mathf.Abs(yOffset);
            float startY = startYPosition + totalHeight / 2f - Mathf.Abs(yOffset) / 2f;

            foreach (var selection in selections.Select((Value, Index) => new { Value, Index}))
            {
                GameObject newButton = Instantiate(buttonPrefab, buttonContainer);
                TextMeshProUGUI buttonText = newButton.GetComponentInChildren<TextMeshProUGUI>();

                if (buttonText != null)
                {
                    buttonText.text = selection.Value;
                }

                // ボタンの位置を調整
                RectTransform rectTransform = newButton.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, startY + yOffset * selection.Index);

                newButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => OnButtonClick(selection.Value));
            }
        }

        void OnButtonClick(string selection)
        {
            // ボタンがクリックされたときの処理
            Debug.Log("選択された: " + selection);

            ClickEvent.Invoke(new EventSelection.PlayerSelection.Body.Selection{
                title = selection,
                playerSelected = true
            });

            HideButton();
        }

        public static void ClearButtons()
        {
            foreach (Transform child in instance.buttonContainer)
            {
                Destroy(child.gameObject);
            }
        }

        public static void ShowButton(List<string> selections)
        {
            instance.GenerateButtons(selections);

            instance.canvas.enabled = true;
        }

        public static void HideButton()
        {
            ClearButtons();

            instance.canvas.enabled = false;
        }
    }
}
