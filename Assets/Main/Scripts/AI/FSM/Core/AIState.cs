using UnityEngine;

namespace Gamaga.AI
{
    public abstract class AIState : State
    {
        protected AIEntity AI { get { return (AIEntity) fsm; } }
    }

}
