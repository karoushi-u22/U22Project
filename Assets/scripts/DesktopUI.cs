using UnityEngine;
using UnityEngine.UI; // UI要素を操作するために追加
using U22Game.Handlers;
using U22Game.Controller;
using U22Game.Events;

namespace U22Game.UI{
    public class DesktopUI : MonoBehaviour
    {
        [SerializeField] private Image deskPC;
        [SerializeField] private Image usbImage;
        [SerializeField] private Image stickyNoteImage;
        [SerializeField] private Image softwareImage;

        [SerializeField] private Sprite usbSprite;
        [SerializeField] private Sprite softwareSprite;
        [SerializeField] private Sprite badusbSprite;
        [SerializeField] private Sprite badsoftwareSprite;

        [SerializeField] private CharacterController2D characterController; // キャラクターの移動を制御するスクリプトへの参照

        private void Start()
        {
            // 初期状態では画像を非表示にする
            SetImageVisibility(deskPC, false);
            SetImageVisibility(usbImage, false);
            SetImageVisibility(stickyNoteImage, false);
            SetImageVisibility(softwareImage, false);

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
            // 不正なアイテムがある場合、対応する画像を設定する
            if (desktopData.IsBadUsb())
            {
                usbImage.sprite = badusbSprite;
            }
            else
            {
                usbImage.sprite = usbSprite;
            }

            if (desktopData.IsBadSoftware())
            {
                softwareImage.sprite = badsoftwareSprite;
            }
            else
            {
                softwareImage.sprite = softwareSprite;
            }

            // 画像を表示する
            SetImageVisibility(deskPC, true);
            SetImageVisibility(usbImage, desktopData.HasUsb());
            SetImageVisibility(stickyNoteImage, desktopData.IsBadStickyNote());
            SetImageVisibility(softwareImage, desktopData.HasInstalledSoftware());

            // キャラクターの移動を制御するスクリプトを一時的に無効にする
            characterController.enabled = false;
        }

        private void Update()
        {
            // ESCキーが押され、かつ画像が表示されている場合に画像を非表示にする
            if (Input.GetKeyDown(KeyCode.Escape) && deskPC.gameObject.activeSelf)
            {
                SetImageVisibility(deskPC, false);
                SetImageVisibility(usbImage, false);
                SetImageVisibility(stickyNoteImage, false);
                SetImageVisibility(softwareImage, false);
                characterController.enabled = true;
            }
        }

        // 画像の表示/非表示を切り替えるメソッド
        private void SetImageVisibility(Image image, bool isVisible)
        {
            image.gameObject.SetActive(isVisible);
        }
    }
}
