using UnityEngine;
using UnityEngine.UI;
using TMPro;
using U22Game.Handlers;
using U22Game.Events;

namespace U22Game.UI
{
    public class PadCheckboxUI : MonoBehaviour
    {
        [SerializeField] private GameObject checkboxPrefab; // チェックボックスのプレハブ
        [SerializeField] private Transform parentTransform; // チェックボックスを配置する親要素のTransform
        [SerializeField] private string[] checkboxTexts; // チェックボックスのテキストの配列

        [SerializeField] private Vector2 startPosition; // チェックボックスの開始位置
        [SerializeField] private Vector2 spacing; // チェックボックスの間隔

        private GameObject[] generatedCheckboxes; // 生成されたチェックボックスを保持する配列

        private SaveDataHandler saveData; // SaveData インスタンスを保持する変数
        private DesktopHandler desktopData;

        private void OnEnable()
        {
            DesktopEvent.OnItemGenerated += HandleItemGenerated;
            DesktopEvent.ExitDeskEvent += HandleExitDeskEvent;
        }

        private void OnDisable()
        {
            DesktopEvent.OnItemGenerated -= HandleItemGenerated;
            DesktopEvent.ExitDeskEvent -= HandleExitDeskEvent;
        }

        // データを読み取るための SaveData インスタンスをセットアップ
        private void SetupSaveDataInstance()
        {
            // DataManager オブジェクトを探して、その SaveData インスタンスを取得
            DataManager dataManager = FindObjectOfType<DataManager>();
            if (dataManager != null)
            {
                saveData = dataManager.saveData;
            }
        }

        private void HandleItemGenerated(string objectName, DesktopHandler desktopData)
        {
            this.desktopData = desktopData;
            // チェックボックスを生成する
            GenerateCheckboxes();

            // SaveData インスタンスをセットアップ
            SetupSaveDataInstance();

            // 保存されたチェックボックスの状態を読み込む
            LoadCheckboxStates(desktopData);
        }

        private void HandleExitDeskEvent()
        {
            // 保存する前にチェックボックスの状態を取得し、デスクトップデータに保存する
            SaveCheckboxStates();

            // 生成されたチェックボックスを非表示にする
            HideCheckboxes();
        }

        private void GenerateCheckboxes()
        {
            // 以前に生成されたチェックボックスを削除
            ClearCheckboxes();

            // チェックボックスの数だけループ
            generatedCheckboxes = new GameObject[checkboxTexts.Length];
            for (int i = 0; i < checkboxTexts.Length; i++)
            {
                // チェックボックスのインスタンスを生成
                GameObject checkboxGO = Instantiate(checkboxPrefab, parentTransform);

                // チェックボックスのテキストを設定
                TextMeshProUGUI checkboxText = checkboxGO.GetComponentInChildren<TextMeshProUGUI>();
                if (checkboxText != null)
                {
                    checkboxText.text = checkboxTexts[i];
                }

                // チェックボックスの位置を調整
                RectTransform checkboxRect = checkboxGO.GetComponent<RectTransform>();
                checkboxRect.anchoredPosition = startPosition + i * spacing;

                // 生成されたチェックボックスを配列に追加
                generatedCheckboxes[i] = checkboxGO;

                // Toggle の onValueChanged イベントに保存処理を追加
                Toggle toggle = checkboxGO.GetComponent<Toggle>();
                if (toggle != null)
                {
                    toggle.onValueChanged.AddListener((isChecked) => SaveCheckboxStates());
                }
            }
        }

        private void ClearCheckboxes()
        {
            // 以前に生成されたチェックボックスを削除
            if (generatedCheckboxes != null)
            {
                foreach (GameObject checkboxGO in generatedCheckboxes)
                {
                    Destroy(checkboxGO);
                }
            }
        }

        private void HideCheckboxes()
        {
            // 生成されたチェックボックスを非表示にする
            if (generatedCheckboxes != null)
            {
                foreach (GameObject checkboxGO in generatedCheckboxes)
                {
                    checkboxGO.SetActive(false);
                }
            }
        }

        // 保存されたチェックボックスの状態を読み込む
        private void LoadCheckboxStates(DesktopHandler desktopData)
        {
            if (desktopData != null)
            {
                for (int i = 0; i < checkboxTexts.Length; i++)
                {
                    // 保存された状態を読み込み
                    bool isChecked = desktopData.GetCheckboxState(checkboxTexts[i]);

                    // チェックボックスの状態を反映
                    Toggle toggle = generatedCheckboxes[i].GetComponent<Toggle>();
                    if (toggle != null)
                    {
                        toggle.isOn = isChecked;
                    }
                }
            }
        }

        // チェックボックスの状態を保存する
        private void SaveCheckboxStates()
        {
            // 保存先のデスクトップデータを取得
            if (saveData != null)
            {
                foreach (GameObject checkboxGO in generatedCheckboxes)
                {
                    // チェックボックスの名前を取得
                    string checkboxName = checkboxGO.GetComponentInChildren<TextMeshProUGUI>().text;

                    // チェックボックスの状態を取得
                    Toggle toggle = checkboxGO.GetComponent<Toggle>();
                    bool isChecked = toggle != null && toggle.isOn;

                    // チェックボックスの状態を保存
                    desktopData.SetCheckboxState(checkboxName, isChecked);
                }
            }
        }

        // イベントトリガーからチェックボックスの状態を変更する
        public void ChangeCheckboxStateOnObjectClick(string objectName)
        {
            if (generatedCheckboxes != null)
            {
                for (int i = 0; i < generatedCheckboxes.Length; i++)
                {
                    // チェックボックスの名前を取得
                    string checkboxName = generatedCheckboxes[i].GetComponentInChildren<TextMeshProUGUI>().text;

                    // 指定されたオブジェクトに関連するチェックボックスか確認
                    if (checkboxName == objectName)
                    {
                        // チェックボックスの状態を反転させる
                        Toggle toggle = generatedCheckboxes[i].GetComponent<Toggle>();
                        if (toggle != null)
                        {
                            toggle.isOn = !toggle.isOn;

                            // 状態の保存
                            SaveCheckboxStates();
                        }

                        // チェックボックスが見つかったらループを抜ける
                        break;
                    }
                }
            }
        }
    }
}
