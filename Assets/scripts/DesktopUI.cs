using UnityEngine;
using UnityEngine.UI; // UI要素を操作するために追加
using U22Game.Handlers;
using U22Game.Controller;
using U22Game.Events;

namespace U22Game.UI{
    public class DesktopUI : MonoBehaviour
    {
        public Image imageToShow;
        private bool isImageVisible = false; // 画像の表示状態を管理するフラグ
        [SerializeField]
        private CharacterController2D characterController; // キャラクターの移動を制御するスクリプトへの参照

        private void Start()
        {
            // 初期状態では画像を非表示にする
            SetImageVisibility(false);

            // DesktopEventスクリプトから発生するイベントを購読
            DesktopEvent.OnItemGenerated += HandleItemGenerated;
        }

        private void OnDestroy()
        {
            // オブジェクトが破棄されるときにイベントの購読を解除
            DesktopEvent.OnItemGenerated -= HandleItemGenerated;
        }

        // イベントハンドラー: アイテムが生成されたときに呼ばれる
        private void HandleItemGenerated(string objectName, DesktopHandler desktopHandler)
        {
            // 画像を表示する
            SetImageVisibility(true);

            // キャラクターの移動を制御するスクリプトを一時的に無効にする
            characterController.enabled = false;
        }

        private void Update()
        {
            // ESCキーが押され、かつ画像が表示されている場合に画像を非表示にする
            if (Input.GetKeyDown(KeyCode.Escape) && isImageVisible)
            {
                SetImageVisibility(false);
                characterController.enabled = true;
            }
        }

        // 画像の表示/非表示を切り替えるメソッド
        private void SetImageVisibility(bool isVisible)
        {
            imageToShow.gameObject.SetActive(isVisible);
            isImageVisible = isVisible;
        }
    }
}
