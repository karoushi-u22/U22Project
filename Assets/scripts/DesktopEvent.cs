using UnityEngine;
using UnityEngine.Events;
using U22Game.Handlers;

namespace U22Game.Events
{
    public class DesktopEvent : MonoBehaviour
    {
        public delegate void ItemGeneratedEventHandler(string objectName, DesktopHandler desktopData);
        public static event ItemGeneratedEventHandler OnItemGenerated;
        public static event UnityAction ExitDeskEvent;

        public int dayData;

        private SaveDataHandler saveData;
        private DesktopHandler desktopData;

        private bool isColliding = false;
        private bool isDeskEvent = false;

        private void Update()
        {
            // Fキーが押され、かつ接触している場合でイベントが発生していない時にイベントを発生させる
            if (Input.GetKeyDown(KeyCode.F) && isColliding && !isDeskEvent)
            {
                Debug.Log("F");

                SetupSaveDataInstance();

                // デスクトップ名に対応するデスクトップデータを取得
                desktopData = saveData.GetDesktopData(dayData, gameObject.name);

                // イベントを発生させる
                if (desktopData != null)
                {
                    OnItemGenerated?.Invoke(gameObject.name, desktopData);
                    isDeskEvent = true;
                }
            }
            // ESCキーが押され、かつOnItemGeneratedが発生している場合にイベントを発生させる
            if (Input.GetKeyDown(KeyCode.Escape) && isDeskEvent)
            {
                ExitDeskEvent?.Invoke();
                isDeskEvent = false;

                Debug.Log("正答数");
                Debug.Log(gameObject.name + "の正答率 : " + saveData.GetDayData(1).GetMatchingCheckboxStateCount(gameObject.name) + " / " + saveData.GetDayData(1).GetCheckboxCount(gameObject.name));
                Debug.Log("全体の正答率 : " + saveData.GetMatchingCheckboxStateCount(1) + " / " + saveData.GetCheckboxCount(1));
                Debug.Log("全体の誤答数 : " + saveData.GetMisscheckedItemsCount(1));
                Debug.Log("全体の見逃した数 : " + saveData.GetUncheckedBadItemsCount(1));

                JsonIoHandler.SaveToJson(saveData);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // 接触した際にフラグを立てる
            isColliding = true;
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            // 接触が終了したらフラグを下げる
            isColliding = false;
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
    }
}
