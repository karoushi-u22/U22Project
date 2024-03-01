using UnityEngine;
using UnityEngine.Events;
using U22Game.Handlers;

namespace U22Game.Events{
    public class DesktopEvent : MonoBehaviour
    {
        public delegate void ItemGeneratedEventHandler(string objectName, DesktopHandler desktopHandler);
        public static event ItemGeneratedEventHandler OnItemGenerated;
        public static event UnityAction ExitDeskEvent;

        private DesktopHandler desktopHandler;

        private bool isColliding = false;
        private bool isDeskEvent = false;

        private void Start()
        {
            // 各オブジェクトごとにDesktopHandlerの新しいインスタンスを作成
            desktopHandler = new DesktopHandler();
            // 不正なアイテムをランダムに生成
            desktopHandler.GenerateBadItems();
        }

        private void Update()
        {
            // Fキーが押され、かつ接触している場合でイベントが発生していない時にイベントを発生させる
            if (Input.GetKeyDown(KeyCode.F) && isColliding && !isDeskEvent)
            {
                Debug.Log("F");
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
