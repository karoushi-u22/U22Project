using UnityEngine;
using U22Game.Handlers;

namespace U22Game.Events{
    public class CharacterCollision : MonoBehaviour
    {
        public delegate void ItemGeneratedEventHandler(string objectName, DesktopHandler desktopHandler);
        public static event ItemGeneratedEventHandler OnItemGenerated;

        private DesktopHandler desktopHandler;

        private bool isColliding = false;

        private void Start()
        {
            // 各オブジェクトごとにDesktopHandlerの新しいインスタンスを作成
            desktopHandler = new DesktopHandler();
            // 不正なアイテムをランダムに生成
            desktopHandler.GenerateBadItems();
        }

        private void Update()
        {
            // Fキーが押され、かつ接触している場合にイベントを発生させる
            if (Input.GetKeyDown(KeyCode.F) && isColliding)
            {
                Debug.Log("F");
                // イベントを発生させる
                OnItemGenerated?.Invoke(gameObject.name, desktopHandler);
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
