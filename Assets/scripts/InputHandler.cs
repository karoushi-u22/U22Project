using UnityEngine;

namespace U22Game.Handlers
{
    public static class InputHandler
    {
        public static Vector2 GetMovementInput()
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal"); // 左右の入力を取得
            float verticalInput = Input.GetAxisRaw("Vertical"); // 上下の入力を取得

            // 入力をベクトルとして返す
            return new Vector2(horizontalInput, verticalInput).normalized;
        }
    }
}
