using UnityEngine;
using Gamaga.CharacterSystem;
using Gamaga.InputSystem;


namespace Gamaga
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Character character = null;
        

        private void Update()
        {
            Vector2 m = PlayerInput.JoystickInput;
            character.HandleInput(m);

            if ( PlayerInput.Jump )
            {
                character.Jump();
            }

            if ( PlayerInput.Attack )
                character.Attack();

        }

        private void OnDisable()
        {
            character.HandleInput(Vector2.zero);
        }

    }

}
