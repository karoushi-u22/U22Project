using UnityEngine;
using U22Game.Handlers;
using U22Game.Events;

namespace U22Game.Utilities{
    public class ItemReader : MonoBehaviour
    {
        private void Start()
        {
            // CharacterCollisionスクリプトから発生するイベントを購読
            CharacterCollision.OnItemGenerated += HandleItemGenerated;
        }

        private void OnDestroy()
        {
            // オブジェクトが破棄されるときにイベントの購読を解除
            CharacterCollision.OnItemGenerated -= HandleItemGenerated;
        }

        // イベントハンドラー: アイテムが生成されたときに呼ばれる
        private void HandleItemGenerated(string objectName, DesktopHandler desktopHandler)
        {
            // アイテム情報とイベントが発生したオブジェクトの名前をログに出力する
            Debug.Log("イベントが発生したオブジェクト名: " + objectName);
            Debug.Log("USBの有無: " + desktopHandler.existsUsb);
            Debug.Log("付箋の有無: " + desktopHandler.existsStickyNote);
            Debug.Log("インストールされたソフトウェアの有無: " + desktopHandler.wasInstalledSoftware);
            Debug.Log("不正なUSBの有無: " + desktopHandler.isBadUsb);
            Debug.Log("不正な付箋の有無: " + desktopHandler.isBadStickyNote);
            Debug.Log("不正なソフトウェアの有無: " + desktopHandler.isBadSoftware);
        }
    }
}
