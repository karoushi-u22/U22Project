using UnityEngine;

namespace U22Game.Handlers{
    public class SaveScoreHandler : MonoBehaviour
    {
        private SaveDataHandler saveData;

        public void SaveScore(int dayData){
            // DataManager オブジェクトを探して、その SaveData インスタンスを取得
            DataManager dataManager = FindObjectOfType<DataManager>();
            if (dataManager != null)
            {
                saveData = dataManager.saveData;
            }

            saveData.CheckboxCnt = saveData.GetCheckboxCount(dayData);
            saveData.SuccessCnt = saveData.GetMatchingCheckboxStateCount(dayData);
            saveData.MissReportCnt = saveData.GetMisscheckedItemsCount(dayData);
            saveData.MissCnt = saveData.GetUncheckedBadItemsCount(dayData);
        }
    }
}
