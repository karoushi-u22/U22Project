using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // 移動速度を設定
    public float gridSize = 1f;  // マップ上の1マスのサイズを設定

    // プレイヤーの移動処理
    void Update()
    {
        // 上下左右のキー入力のみを処理する
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            moveY = 1f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveY = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveX = 1f;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            moveX = -1f;
        }

        // 移動量を1マスの大きさに調整
        Vector2 movement = new Vector2(moveX, moveY).normalized * gridSize;

        // 移動量を計算して、現在の位置に加算
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }
}
