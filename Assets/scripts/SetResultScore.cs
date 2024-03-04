using UnityEngine;
using TMPro;
using U22Game.Handlers;

public class SetResultScore : MonoBehaviour
{
    // テキストを変更したい子オブジェクトの参照を配列として持つ
    public TextMeshProUGUI[] textObjects;

    // 新しいテキストを設定するメソッド
    public void SetTextResultScore()
    {
        foreach (TextMeshProUGUI textObject in textObjects)
        {
            if (textObject != null)
            {
                if (textObject.gameObject.name == "SuccesCNT")
                {
                    textObject.text = SaveData.successCnt.ToString();
                }
                else if (textObject.gameObject.name == "MissReportCNT")
                {
                    textObject.text = SaveData.missReportCnt.ToString();
                }
                else if (textObject.gameObject.name == "MissCNT")
                {
                    textObject.text = SaveData.missCnt.ToString();
                }
            }
            else
            {
                Debug.LogError("TextMeshProUGUIオブジェクトが見つかりません。");
            }
        }
    }

    // 例: Start() メソッドで全てのテキストを変更する場合
    private void Start()
    {
        SetTextResultScore();
    }
}
