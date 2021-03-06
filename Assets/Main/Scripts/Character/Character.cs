﻿using UnityEngine;
using Gamaga.DamageSystem;

namespace Gamaga.CharacterSystem
{
    /// <summary>
    /// Class for a character that can move and jump, the character is drive using forces
    /// </summary>
    public class Character : Actor
    {
        [SerializeField] private CharacterStats stats = null;
        [SerializeField] private LayerMask groundLayerMask;
        [SerializeField] private bool airControll = false;
        [SerializeField] private float hitTime = 1.0f;
        [SerializeField] private GameObject[] dealDamageArray = null;

        //character state bools
        private bool isJumping = false;
        private bool isFacingRight = true;
        private bool isGrounded = false;
        private bool isRunning = true;
        private bool isHit = false;
        private bool isDead = false;

        private float hitTimer = 0.0f;
        private int numberOfJumps = 0;
        private float ungroundedTimer = 0.0f;

        private Vector2 currentInput = Vector2.zero;


        protected override void Awake()
        {
            base.Awake();

            //setup damageable for handling damages to this character
            SetUpDamageable(stats.HP);

            //setup my deal damage parts
            SetUpDealDamageArray( stats.DMG );
        }

        private void SetUpDamageable(int hp)
        {
            DamageableManager damageable = gameObject.GetOrAddComponent<DamageableManager>();
            damageable.SetHP(hp);
        }

        private void SetUpDealDamageArray(int dmg)
        {
            for (int n = 0; n < dealDamageArray.Length; n++)
            {
                IDealDamage dealDamagePart = dealDamageArray[n].GetComponent<IDealDamage>();
                dealDamagePart.SetDamage(dmg);
            }
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
            currentInput = m;

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

        public void Attack()
        {
            if (!isGrounded || IsPlayingAttackAnimation())
                return;

            rb.velocity = Vector2.zero;           
            animator.SetTrigger("Attack");
        }

        private void StopAndAddForce(Vector2 force , ForceMode2D forceMode)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(force, forceMode);
        }


        private bool CanMove()
        {
            if (IsPlayingAttackAnimation())
                return false;


            return !isDead && (isGrounded || CanDoAirControl()) ;
        }

        private bool CanDoAirControl()
        {
            if (!airControll)
                return false;

            //because the player is drive using tb.velocity directly, the player can get stuck while jumping pushing throung a platform or wall
            //and just get stuck there until he stop moving in the air, in order to avoid this we need to check if the player is trying to push
            //in the air to a wall or platform, and we do it just making a capsule cast using the player collider just a little far away if he 
            //is touching someting them we stop the air control

            Vector2 center = transform.position;
            center += thisCollider.offset;

            float angle = transform.eulerAngles.z;
            int layer = 1 << LayerMask.NameToLayer("Ground");

            return !Physics2D.CapsuleCast(center, thisCollider.size, thisCollider.direction, angle, currentInput.normalized, 0.1f, layer ) ;            

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

            numberOfJumps++;
            //ensure that at leats for this frame we no are grounded so the jump counter dont get to zero inmediatly
            ungroundedTimer = 0.01f;
            StopAndAddForce(stats.JumpForce * Vector2.up , ForceMode2D.Force);
        }

        private bool CanJump()
        {            
            if (IsPlayingAttackAnimation())
                return false;
           
            return !isDead && ( (isGrounded || numberOfJumps < stats.MaxNumberOfJumps) && !isHit );
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

            if (isGrounded && numberOfJumps > 0)
            {
                numberOfJumps = 0;
            }

            if (isJumping && isGrounded)
            {
                isJumping = false;
            }

        }

        private bool IsTouchingTheGround()
        {
            if (ungroundedTimer > 0.0f)
            {
                ungroundedTimer -= Time.deltaTime;
                return false;
            }

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

            rb.velocity = Vector2.zero;

            if (isGrounded)
            {
                rb.isKinematic = true;
                Destroy(thisCollider);
            }
                
        }        

    }

}
