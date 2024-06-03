using UnityEngine;

namespace U22Game.Handlers{
    public class SaveScoreHandler : MonoBehaviour
    {
        private SaveDataHandler saveData;

        public void SaveScore(int dayData){
            // DataManager オブジェクトを探して、その SaveData インスタンスを取得
            saveData = JsonIoHandler.LoadFromJson();

            saveData.CheckboxCnt = saveData.GetCheckboxCount(dayData);
            saveData.SuccessCnt = saveData.GetMatchingCheckboxStateCount(dayData);
            saveData.MissReportCnt = saveData.GetMisscheckedItemsCount(dayData);
            saveData.MissCnt = saveData.GetUncheckedBadItemsCount(dayData);

            JsonIoHandler.SaveToJson(saveData);
        }
    }
}
