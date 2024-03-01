using UnityEngine;
using UnityEngine.Events;
using U22Game.Handlers;

namespace U22Game.Events{
    public class DesktopEvent : MonoBehaviour
    {
        public delegate void ItemGeneratedEventHandler(string objectName, DesktopData desktopData);
        public static event ItemGeneratedEventHandler OnItemGenerated;
        public static event UnityAction ExitDeskEvent;

        [SerializeField] private string desktopName;
        [SerializeField] private int dayData;

        private SaveData saveData;
        private DesktopData desktopData;

        private bool isColliding = false;
        private bool isDeskEvent = false;

        private void Start()
        {
            // SaveData インスタンスを取得
            saveData = FindObjectOfType<DataManager>().saveData;
        }

        private void Update()
        {
            // Fキーが押され、かつ接触している場合でイベントが発生していない時にイベントを発生させる
            if (Input.GetKeyDown(KeyCode.F) && isColliding && !isDeskEvent)
            {
                Debug.Log("F");

                // デスクトップ名に対応するデスクトップデータを取得
                desktopData = saveData.GetDesktopData(dayData, desktopName);

                // イベントを発生させる
                OnItemGenerated?.Invoke(gameObject.name, desktopData);
                isDeskEvent = true;
            }
            // ESCキーが押され、かつOnItemGeneratedが発生している場合にイベントを発生させる
            if (Input.GetKeyDown(KeyCode.Escape) && isDeskEvent)
            {
                ExitDeskEvent?.Invoke();
                isDeskEvent = false;
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
    }
}
