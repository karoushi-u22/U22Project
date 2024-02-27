using UnityEngine;
using UnityEngine.UI;

public class ToggleArrayCreator : MonoBehaviour
{
    public Toggle togglePrefab; // ヒエラルキーでToggleプレハブを割り当てる
    public Transform toggleParent; // ヒエラルキーでToggleを配置する親要素を割り当てる
    public string[] toggleLabels; // Toggleに表示するラベルの文字列配列

    void Start()
    {
        // 配列の要素分Toggleを生成
        CreateToggles();
    }

    void CreateToggles()
    {
        // Toggleプレハブが指定されていない場合は終了
        if (togglePrefab == null)
        {
            Debug.LogError("Toggle Prefab is not assigned!");
            return;
        }

        // 配列の要素分Toggleを生成
        for (int i = 0; i < toggleLabels.Length; i++)
        {
            // Toggleを生成
            Toggle toggle = Instantiate(togglePrefab, toggleParent);

            // Toggleのラベルを設定
            toggle.GetComponentInChildren<Text>().text = toggleLabels[i];

            // Toggleの配置やサイズの調整（適宜調整が必要）
            RectTransform toggleRT = toggle.GetComponent<RectTransform>();
            toggleRT.anchoredPosition = new Vector2(0, -i * toggleRT.sizeDelta.y);
        }
    }
}
