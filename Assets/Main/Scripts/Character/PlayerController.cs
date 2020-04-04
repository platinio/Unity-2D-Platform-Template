using UnityEngine;
using Gamaga.CharacterSystem;
using Gamaga.InputSystem;
using Gamaga.DamageSystem;
using Gamaga.GameLogic;

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

        }

    }

}
