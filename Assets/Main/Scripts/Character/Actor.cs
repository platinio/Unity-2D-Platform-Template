using UnityEngine;

namespace Gamaga
{
    public class Actor : MonoBehaviour
    {
        [SerializeField] protected BoxCollider2D groundedCollider = null;
        [SerializeField] [Range(0.1f, 1.0f)] private float animatorSpeed = 1.0f;

        protected Rigidbody2D rb = null;
        protected SpriteRenderer render = null;
        protected Animator animator = null;

        protected int isFacingRightHash = Animator.StringToHash("isFacingRight");
        protected int isJumpingHash = Animator.StringToHash("isJumping");
        protected int isGroundedHash = Animator.StringToHash("isGrounded");
        protected int isRunningHash = Animator.StringToHash("isRunning");
        protected int isDeadHash = Animator.StringToHash("isDead");

        protected virtual void Awake()
        {
            GetReferences();
        }

        private void GetReferences()
        {
            rb = GetComponent<Rigidbody2D>();
            render = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();

            Initialize();
        }

        private void Initialize()
        {
            groundedCollider.enabled = false;
            animator.speed = animatorSpeed;
        }

    }
}

