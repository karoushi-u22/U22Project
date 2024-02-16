using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f; // 移動速度を設定
    [SerializeField]
    private float gridSize = 1f;  // マップ上の1マスのサイズを設定

    private bool isMoving = false; // プレイヤーが移動中かどうかを追跡する変数

    void Update()
    {
        // 移動中は入力を受け付けない
        if (isMoving)
            return;

        // 上下左右のキー入力のみを処理する
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            moveY = gridSize;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveY = -gridSize;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveX = gridSize;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            moveX = -gridSize;
        }

        // 移動開始
        if (moveX != 0 || moveY != 0)
        {
            StartCoroutine(MoveToNextGrid(new Vector2(moveX, moveY)));
        }
    }

    // 次のマスへの移動を徐々に行うコルーチン
    private System.Collections.IEnumerator MoveToNextGrid(Vector2 direction)
    {
        isMoving = true;

        // 現在の位置から移動先を計算
        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition + (Vector3)direction;

        // 移動先をグリッドの中心に丸める
        endPosition.x = Mathf.Floor(endPosition.x / gridSize) * gridSize + gridSize / 2f;
        endPosition.y = Mathf.Floor(endPosition.y / gridSize) * gridSize + gridSize / 2f;

        // 移動先にコリジョンがあるか確認
        Collider2D[] colliders = Physics2D.OverlapCircleAll(endPosition, gridSize / 10f); // 移動先座標上にコリジョンがあるか判定
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != gameObject) // 自分以外のコリジョンがある場合、移動を中止
            {
                Debug.Log("移動先にコリジョンがあります。移動を中止します。");
                isMoving = false;
                yield break;
            }
        }

        // デバッグログ：移動先の位置を表示
        Debug.Log("移動先の位置: " + endPosition);

        // 移動開始
        float sqrDistance = (endPosition - startPosition).sqrMagnitude; // 移動する距離の二乗を計算
        float distanceTraveled = 0f;

        while (distanceTraveled < sqrDistance)
        {
            // 移動速度に応じて次の位置を計算し、プレイヤーを移動させる
            float deltaMove = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, endPosition, deltaMove);

            // 移動した距離を更新
            distanceTraveled = (transform.position - startPosition).sqrMagnitude;

            yield return null;
        }

        // 移動が完了したら移動中フラグをオフにする
        isMoving = false;
    }
}
