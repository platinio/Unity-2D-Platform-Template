using UnityEngine;
using Gamaga.CharacterSystem;
using Gamaga.DamageSystem;
using Gamaga.GameLogic;



namespace Gamaga.AI
{
    public class AIEntity : FSM
    {
        [SerializeField] private Character character = null;
        [SerializeField] private float walkSpeed = 0.0f;


        public void MoveLeft()
        {
            character.HandleInput( new Vector2( -walkSpeed , 0.0f ) );
        }

        public void MoveRight()
        {
            character.HandleInput(new Vector2( walkSpeed, 0.0f));
        }

        public void Stop()
        {
            character.HandleInput(new Vector2(0.0f, 0.0f));
        }


    }
}
