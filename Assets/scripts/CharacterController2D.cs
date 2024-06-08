    using UnityEngine;
    using U22Game.Handlers;

    namespace U22Game.Controller{
        public class CharacterController2D : MonoBehaviour
        {
            public float moveSpeed = 2f; // キャラクターの移動速度

            private Rigidbody2D rb;
            private Animator animator;
            
            void Start()
            {
                rb = GetComponent<Rigidbody2D>();
                animator = GetComponent<Animator>();
            }

            // Update is called once per frame
            void Update()
            {
                Vector2 movementInput = InputHandler.GetMovementInput(); // 入力を取得

                // 単方向のみの入力に制限する
                if (Mathf.Abs(movementInput.x) > 0 && Mathf.Abs(movementInput.y) > 0)
                {
                    if (Mathf.Abs(movementInput.x) > Mathf.Abs(movementInput.y))
                    {
                        movementInput.y = 0f;
                    }
                    else
                    {
                        movementInput.x = 0f;
                    }
                }

                // 移動ベクトルに速度を掛けて移動させる
                rb.velocity = movementInput * moveSpeed;
                animator.keepAnimatorStateOnDisable = true;

                // アニメーションの設定
                if (movementInput != Vector2.zero)
                {
                    animator.SetFloat("Horizontal", movementInput.x);
                    animator.SetFloat("Vertical", movementInput.y);
                    animator.SetBool("IsMoving", true);
                }
                else
                {
                    animator.SetBool("IsMoving", false);
                }
            }
        }
    }




                      