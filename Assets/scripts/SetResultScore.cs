using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace U22Game.Handlers
{
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

        private SaveDataHandler saveData;

        // 新しいテキストを設定するメソッド
        public void SetTextResultScore(SaveDataHandler saveData)
        {
            foreach (TextMeshProUGUI textObject in textObjects)
            {
                if (textObject != null)
                {
                    if (textObject.gameObject.name == "SuccesCNT")
                    {
                        textObject.text = saveData.SuccessCnt.ToString();
                    }
                    else if (textObject.gameObject.name == "MissReportCNT")
                    {
                        textObject.text = saveData.MissReportCnt.ToString();
                    }
                    else if (textObject.gameObject.name == "MissCNT")
                    {
                        textObject.text = saveData.MissCnt.ToString();
                    }
                }
                else
                {
                    Debug.LogError("TextMeshProUGUIオブジェクトが見つかりません。");
                }
            }
        }

        // 正答率でクリアランク画像を変更するメソッド
        public void SetClearLankImage(SaveDataHandler saveData)
        {
            float checkboxCnt = saveData.CheckboxCnt;
            float successCnt = saveData.SuccessCnt;

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
            saveData = JsonSaveLoadHandler.LoadFromJson();
            SetTextResultScore(saveData);
            SetClearLankImage(saveData);
        }
    }
}
