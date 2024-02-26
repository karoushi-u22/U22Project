using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ImageClickHandler : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("クリックしました。");
        // ここにクリック時の追加の処理を記述することも可能
    }
}
