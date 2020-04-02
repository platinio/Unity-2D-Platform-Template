using UnityEngine;

namespace Gamaga.AI
{
    public class IdleState : AIState
    {
        [SerializeField] private float minIdleTime = 1.0f;
        [SerializeField] private float maxIdleTime = 5.0f;

        private float idleTimer = 0.0f;

        public override void OnEnterState()
        {
            base.OnEnterState();
            idleTimer = Random.Range( minIdleTime , maxIdleTime );
        }

        public override int GetStateIndex()
        {
            return (int)AIStateType.Idle;
        }

        public override int OnUpdate()
        {
            idleTimer -= Time.deltaTime;

            if (idleTimer > 0.0f)
                return (int)AIStateType.Idle;

            return (int)AIStateType.Patrol;

        }
    }

}


