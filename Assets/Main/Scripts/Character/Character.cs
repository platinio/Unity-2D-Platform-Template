using UnityEngine;

namespace Gamaga.CharacterSystem
{
    public class Character : Actor
    {
        private bool isJumping = false;
        private bool movingLeft = false;
        private bool movingRight = false;

        private void Update()
        {
            UpdateAnimator();
        }

        public void MoveLeft(float m)
        {
            rb.MovePosition(rb.position + ( Vector2.left * m * Time.deltaTime ));
        }

        public void MoveRight(float m)
        {
            rb.MovePosition(rb.position + (Vector2.right * m * Time.deltaTime));
        }

        public void Jump(float force)
        {
            rb.AddForce( Vector2.up * force );
        }

        private void UpdateAnimator()
        {
            
        }

    }

}
