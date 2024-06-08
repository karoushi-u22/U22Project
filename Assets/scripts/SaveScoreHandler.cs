using UnityEngine;

namespace U22Game.Handlers{
    public class SaveScoreHandler : MonoBehaviour
    {
        private SaveDataHandler saveData;

        public void SaveScore(){
            // DataManager オブジェクトを探して、その SaveData インスタンスを取得
            saveData = JsonIoHandler.LoadFromJson();

            saveData.CheckboxCnt = saveData.GetCheckboxCount(saveData.CurrentDate);
            saveData.SuccessCnt = saveData.GetMatchingCheckboxStateCount(saveData.CurrentDate);
            saveData.MissReportCnt = saveData.GetMisscheckedItemsCount(saveData.CurrentDate);
            saveData.MissCnt = saveData.GetUncheckedBadItemsCount(saveData.CurrentDate);

            JsonIoHandler.SaveToJson(saveData);
        }
    }
}
