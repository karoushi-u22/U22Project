using UnityEngine;
using UnityEngine.EventSystems;

public class ImageHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // マウスが画像に重なった時の処理
        MoveImageUp();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // マウスが画像から離れた時の処理
        MoveImageDown();
    }

    private void MoveImageUp()
    {
        // 画像をY軸方向に10上に移動
        rectTransform.localPosition += new Vector3(0, 200, 0);
    }

    private void MoveImageDown()
    {
        // 画像を元の位置に戻す
        rectTransform.localPosition -= new Vector3(0, 200, 0);
    }
}
