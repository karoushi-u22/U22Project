using UnityEngine;

namespace U22Game.Controller
{
    public class CharacterAnimationController : MonoBehaviour
    {
        Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = (x == 0) ? Input.GetAxisRaw("Vertical") : 0.0f;

            if (x != 0 || y != 0)
            {
                animator.SetBool("isMoving", true);
                animator.SetFloat("x", x);
                animator.SetFloat("y", y);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }
        }
    }
}
