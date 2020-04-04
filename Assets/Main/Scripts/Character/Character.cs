using UnityEngine;
using Gamaga.DamageSystem;

namespace Gamaga.CharacterSystem
{
    public class Character : Actor
    {
        [SerializeField] private CharacterStats stats = null;
        [SerializeField] private LayerMask groundLayerMask;
        [SerializeField] private bool airControll = false;
        [SerializeField] private float hitTime = 1.0f;        
        

        private bool isJumping = false;
        private bool isFacingRight = true;
        private bool isGrounded = false;
        private bool isRunning = true;
        private bool isHit = false;
        private bool isDead = false;
                
        private float hitTimer = 0.0f;
        
        protected override void Awake()
        {
            base.Awake();
            SetUpDamageable( stats );
        }

        private void SetUpDamageable(CharacterStats stats)
        {
            DamageableManager damageable = gameObject.GetOrAddComponent<DamageableManager>();
            damageable.SetHP(stats.HP);            
        }

        private void Update()
        {            
            UpdateAnimator();
            UpdateCharacterState();
            UpdateHitTimer();
        }

        private void UpdateHitTimer()
        {
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

            m *= stats.Speed;

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

        public void HandleDamage(DamageInfo info)
        {
            if (isDead)
                return;

            hitTimer = hitTime;
            isHit = true;
            isRunning = false;
            animator.SetTrigger("Hit");

            StopAndAddForce( info.force * info.dir , info.forceMode );
            
        }

        private void StopAndAddForce(Vector2 force , ForceMode2D forceMode)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(force, forceMode);
        }


        private bool CanMove()
        {
            return !isDead && (isGrounded || airControll);
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

        public void Jump()
        {
            if (!CanJump())
                return;

            StopAndAddForce(stats.JumpForce * Vector2.up , ForceMode2D.Force);
        }

        private bool CanJump()
        {
            return !isDead && (isGrounded && !isHit);
        }

        private void UpdateAnimator()
        {
            animator.SetBool( isJumpingHash , isJumping );            
            animator.SetBool( isGroundedHash , isGrounded );
            animator.SetBool( isRunningHash , isRunning );
            animator.SetBool( isDeadHash , isDead );
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

        public void Kill()
        {
            isDead = true;
        }

    }

}
