using UnityEngine;
using U22Game.Handlers;
using U22Game.Events;

namespace U22Game.Utilities{
    public class ItemReader : MonoBehaviour
    {
        private void Start()
        {
            // DesktopEventスクリプトから発生するイベントを購読
            DesktopEvent.OnItemGenerated += HandleItemGenerated;
        }

        private void OnDestroy()
        {
            // オブジェクトが破棄されるときにイベントの購読を解除
            DesktopEvent.OnItemGenerated -= HandleItemGenerated;
        }

        // イベントハンドラー: アイテムが生成されたときに呼ばれる
        private void HandleItemGenerated(string objectName, DesktopData desktopData)
        {
            // アイテム情報とイベントが発生したオブジェクトの名前をログに出力する
            Debug.Log("イベントが発生したオブジェクト名: " + objectName);
            Debug.Log("USBの有無: " + desktopData.HasUsb());
            Debug.Log("付箋の有無: " + desktopData.HasStickyNote());
            Debug.Log("インストールされたソフトウェアの有無: " + desktopData.HasInstalledSoftware());
            Debug.Log("不正なUSBの有無: " + desktopData.IsBadUsb());
            Debug.Log("不正な付箋の有無: " + desktopData.IsBadStickyNote());
            Debug.Log("不正なソフトウェアの有無: " + desktopData.IsBadSoftware());
        }
    }
}
