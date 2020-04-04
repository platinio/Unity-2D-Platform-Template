using UnityEngine;
using Gamaga.CharacterSystem;
using Gamaga.InputSystem;

namespace Gamaga
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Character character = null;
        [SerializeField] private float speed = 5.0f;
        [SerializeField] private float jumpForce = 50.0f;

        private void Update()
        {
            Vector2 m = PlayerInput.JoystickInput;
            character.HandleInput(m * speed);

            if ( PlayerInput.Jump )
            {
                character.Jump(jumpForce);
            }

        }

    }

}
