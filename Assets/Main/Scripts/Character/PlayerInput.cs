using UnityEngine;
using Gamaga.CharacterSystem;

namespace Gamaga
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private Character character = null;
        [SerializeField] private float speed = 5.0f;
        [SerializeField] private float jumpForce = 50.0f;

        private void Update()
        {
            Vector2 m = new Vector2( Input.GetAxisRaw("Horizontal") , Input.GetAxisRaw("Vertical") );
            character.HandleInput(m * speed);

            if (Input.GetButtonDown("Jump"))
            {
                character.Jump(jumpForce);
            }

        }

    }

}
