using UnityEngine;

namespace Gamaga.CharacterSystem
{
    /// <summary>
    /// Base class for all characters
    /// </summary>
    public class Actor : MonoBehaviour
    {
        [SerializeField] protected BoxCollider2D groundedCollider = null;
        [SerializeField] [Range(0.1f, 1.0f)] private float animatorSpeed = 1.0f;

        protected Rigidbody2D rb = null;
        protected SpriteRenderer render = null;
        protected Animator animator = null;
        protected CapsuleCollider2D thisCollider = null;

        //animator hash
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
            thisCollider = GetComponent<CapsuleCollider2D>();

            Initialize();
        }

        private void Initialize()
        {
            groundedCollider.enabled = false;
            animator.speed = animatorSpeed;
        }

        protected bool IsPlayingAttackAnimation()
        {
            return animator.GetCurrentAnimatorStateInfo(0).IsName("Attack");
        }


    }
}

