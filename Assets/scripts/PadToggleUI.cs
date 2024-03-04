using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace U22Game.UI{
    public class PadToggleUI : MonoBehaviour, IPointerClickHandler
    {
        // 変更したいImageのRectTransform
        [SerializeField] private RectTransform imageRectTransform;

        // トグルする位置
        [SerializeField] private Vector2 position1;
        [SerializeField] private Vector2 position2;

        // アニメーションの速度
        [SerializeField] private float moveSpeed = 5f;

        // トグル状態を保持する変数
        private bool isPosition = true;

        public void OnPointerClick(PointerEventData eventData)
        {
            // トグルされた位置に移動
            if (!IsMoving())
            {
                isPosition = !isPosition;
                Vector2 targetPosition = isPosition ? position1 : position2;
                StartCoroutine(MoveToPosition(targetPosition));
            }
        }

        private bool IsMoving()
        {
            return imageRectTransform.anchoredPosition != (isPosition ? position1 : position2);
        }

        private IEnumerator MoveToPosition(Vector2 targetPosition)
        {
            while (Vector2.Distance(imageRectTransform.anchoredPosition, targetPosition) > 0.1f)
            {
                imageRectTransform.anchoredPosition = Vector2.Lerp(imageRectTransform.anchoredPosition, targetPosition, moveSpeed * Time.deltaTime);
                yield return null;
            }

            imageRectTransform.anchoredPosition = targetPosition;
        }
    }
}
