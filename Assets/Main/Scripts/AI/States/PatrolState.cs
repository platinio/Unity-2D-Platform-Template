using UnityEngine;

namespace Gamaga.AI
{
    /// <summary>
    /// Patrol State of the skeleton
    /// </summary>
    public class PatrolState : AIState
    {
        [SerializeField] private Transform leftPatrolPoint = null;
        [SerializeField] private Transform rightPatrolPoint = null;
        [SerializeField] private float minDistanceToPatrolPoint = 0.25f;

        private Transform nextPatrolPoint = null;

        public override void OnEnterState()
        {
            base.OnEnterState();
            nextPatrolPoint = GetNextPatrolPoint();
        }

        public override int GetStateIndex()
        {
            return (int)AIStateType.Patrol;
        }

        public override int OnUpdate()
        {
            MoveToPatrolPoint();

            if (IsCloseEnoughToPatrolPoint())
                return (int)AIStateType.Idle;

            return (int)AIStateType.Patrol;

        }

        private void MoveToPatrolPoint()
        {
            Vector2 dir = CalculateDirection();

            if (dir.x > 0)
            {
                AI.MoveRight();
            }
            else
            {
                AI.MoveLeft();
            }
        }

        private Vector2 CalculateDirection()
        {
            return (nextPatrolPoint.position - transform.position).normalized; ;
        }

        private bool IsCloseEnoughToPatrolPoint()
        {
            float distanceToPatrolPoint = Mathf.Abs(transform.position.x - nextPatrolPoint.position.x);

            if (distanceToPatrolPoint < minDistanceToPatrolPoint)
            {
                return true;
            }

            return false;
        }

        private Transform GetNextPatrolPoint()
        {
            float distanceToLeftPattrol = Vector2.Distance( transform.position , leftPatrolPoint.position );
            float distanceToRightPatrol = Vector2.Distance(transform.position, rightPatrolPoint.position);

            return distanceToRightPatrol > distanceToLeftPattrol ? rightPatrolPoint : leftPatrolPoint;
        }
    }

}

