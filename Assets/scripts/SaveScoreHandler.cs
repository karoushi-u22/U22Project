using UnityEngine;

namespace U22Game.Handlers{
    public class SaveScoreHandler : MonoBehaviour
    {
        private SaveData saveData;

        public void SaveScore(int dayData){
            // DataManager オブジェクトを探して、その SaveData インスタンスを取得
            DataManager dataManager = FindObjectOfType<DataManager>();
            if (dataManager != null)
            {
                saveData = dataManager.saveData;
            }

            SaveData.successCnt = saveData.GetMatchingCheckboxStateCount(dayData);
            SaveData.missReportCnt = saveData.GetMisscheckedItemsCount(dayData);
            SaveData.missCnt = saveData.GetUncheckedBadItemsCount(dayData);
        }
    }
}
