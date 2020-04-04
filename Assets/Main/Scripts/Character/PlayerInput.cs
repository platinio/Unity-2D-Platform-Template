
using UnityEngine;

namespace Gamaga.InputSystem
{
    public static class PlayerInput 
    {
        public static Vector2 JoystickInput
        {
            get 
            {
                #if UNITY_STANDALONE || UNITY_EDITOR
                Vector2 input = new Vector2( Input.GetAxisRaw("Horizontal") , Input.GetAxisRaw("Vertical") );
                return input;
                #else
                Vector2 input = new Vector2(InputManager.GetAxis("Horizontal"), InputManager.GetAxis("Vertical"));
                return input;
                #endif
            }
        }

        public static bool Jump
        {
            get
            {
                #if UNITY_STANDALONE || UNITY_EDITOR
                return Input.GetButtonDown("Jump");
                #else
                return InputManager.GetAxis("Jump") > 0.5f;
                #endif
            }
        }

    }

}
