using UnityEngine;

namespace Gamaga.CharacterSystem
{
    public class Character : Actor
    {
        [SerializeField] private LayerMask groundLayerMask;
        [SerializeField] private bool airControll = false;
        [SerializeField] private bool debugModeOn = false;

        private bool isJumping = false;
        private bool isFacingRight = true;
        private bool isGrounded = false;
        private bool isRunning = true;
        private bool isHit = false;
        
        private const float HIT_TIME = 1.0f;
        private float hitTimer = 0.0f;

        public bool DebugModeOn { get { return debugModeOn; } }

        private void Update()
        {            
            UpdateAnimator();
            UpdateCharacterState();

            if (hitTimer > 0.0f)
            {
                hitTimer -= Time.deltaTime;
            }
            else
            {
                isHit = false;
            }
                

        }

        public void HandleInput(Vector2 m)
        {          

            if (isHit)
                return;

            if (Mathf.Abs(m.x) < 0.01f && CanMove())
            {
                Stop();
            }
            else if( CanMove() )
            {
                Move(m);
                SetFacingDirection();
            }
        }

        public void HandleHit(Vector2 force , ForceMode2D forceMode)
        {
            hitTimer = HIT_TIME;
            isHit = true;
            Debug.Log("setting trigger");
            animator.SetTrigger("Hit");
            rb.AddForce(force , forceMode);
        }

        private bool CanMove()
        {
            return isGrounded || airControll;
        }

        private void Move(Vector2 m)
        {            
            m.y = rb.velocity.y;
            rb.velocity = m;
            isRunning = true;
        }

        private void SetFacingDirection()
        {
            if (rb.velocity.x > 0.0f && !isFacingRight)
            {
                Flip();
            }
            else if (rb.velocity.x < 0.0f && isFacingRight)
            {
                Flip();
            }
        }

        private void Flip()
        {
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;

            isFacingRight = !isFacingRight;
        }

        private void Stop()
        {
            rb.velocity = new Vector2( 0.0f , rb.velocity.y );
            isRunning = false;
        }

        public void Jump(float force)
        {
            rb.AddForce( Vector2.up * force );
        }

        private void UpdateAnimator()
        {
            animator.SetBool( isJumpingHash , isJumping );            
            animator.SetBool( isGroundedHash , isGrounded );
            animator.SetBool( isRunningHash , isRunning );
        }

        private void UpdateCharacterState()
        {
            isGrounded = IsTouchingTheGround();

            if (isJumping && isGrounded)
            {
                isJumping = false;
            }

        }

        private bool IsTouchingTheGround()
        {
            Collider2D hitCollider = GroundColliderOverlap();
            return hitCollider != null;
        }

        private Collider2D GroundColliderOverlap()
        {
            return Physics2D.OverlapBox(groundedCollider.transform.position, groundedCollider.size, groundedCollider.transform.eulerAngles.z, groundLayerMask.value);
        }


    }

}
