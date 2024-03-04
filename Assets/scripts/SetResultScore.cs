using UnityEngine;
using UnityEngine.UI;
using TMPro;
using U22Game.Handlers;

public class SetResultScore : MonoBehaviour
{
    // テキストを変更したい子オブジェクトの参照を配列として持つ
    [SerializeField] private TextMeshProUGUI[] textObjects;
    [SerializeField] private Image succesLank;
    [SerializeField] private Sprite lankS;
    [SerializeField] private int lankSRate;
    [SerializeField] private Sprite lankA;
    [SerializeField] private int lankARate;
    [SerializeField] private Sprite lankB;
    [SerializeField] private int lankBRate;
    [SerializeField] private Sprite lankC;

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

    // 正答率でクリアランク画像を変更するメソッド
    public void SetClearLankImage()
    {
        float checkboxCnt =  SaveData.checkboxCnt;
        float successCnt = SaveData.successCnt;

        float successRate = successCnt / checkboxCnt * 100;

        if (successRate >= lankSRate)
        {
            succesLank.sprite = lankS;
        }
        else if (successRate >= lankARate)
        {
            succesLank.sprite = lankA;
        }
        else if (successRate >= lankBRate)
        {
            succesLank.sprite = lankB;
        }
        else if (successRate >= 0)
        {
            succesLank.sprite = lankC;
        }
    }

    // 例: Start() メソッドで全てのテキストを変更する場合
    private void Start()
    {
        SetTextResultScore();
        SetClearLankImage();
    }
}
