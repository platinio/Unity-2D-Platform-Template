using UnityEngine;

namespace Gamaga.AI
{
    /// <summary>
    /// Base class for all AIStates
    /// </summary>
    public abstract class AIState : State
    {
        protected AIEntity AI { get { return (AIEntity) fsm; } }
    }

}
