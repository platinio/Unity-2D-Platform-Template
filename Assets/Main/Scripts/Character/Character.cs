using UnityEngine;

namespace Gamaga.CharacterSystem
{
    public class Character : Actor
    {
        [SerializeField] private LayerMask groundLayerMask;
        [SerializeField] private bool airControll = false;

        private bool isJumping = false;
        private bool isMovingRight = true;
        private bool isGrounded = false;

        private Vector2 velocitySmooth = Vector2.zero;

        private void Update()
        {
            UpdateAnimator();
            UpdateGroundedState();
        }

        public void HandleInput(Vector2 m)
        {
            if (m.magnitude < 0.01f && CanMove())
            {
                Stop();
            }
            else if( CanMove() )
            {
                Move(m);
                SetFacingDirection();
            }
        }

        private bool CanMove()
        {
            return isGrounded || airControll;
        }

        private void Move(Vector2 m)
        {            
            m.y = rb.velocity.y;
            rb.velocity = m;
        }

        private void SetFacingDirection()
        {
            isMovingRight = rb.velocity.x > 0.0f;
        }

        private void Stop()
        {
            rb.velocity = new Vector2( 0.0f , rb.velocity.y );
        }

        public void Jump(float force)
        {
            rb.AddForce( Vector2.up * force );
        }

        private void UpdateAnimator()
        {
            
        }

        private void UpdateGroundedState()
        {
            isGrounded = IsTouchingTheGround();            
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
