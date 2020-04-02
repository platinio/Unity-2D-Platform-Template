using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamaga.AI
{
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
            Vector2 dir = (nextPatrolPoint.position - transform.position).normalized;

            if (dir.x > 0)
            {
                AI.MoveRight();
            }
            else
            {
                AI.MoveLeft();
            }

            float distanceToPatrolPoint = Mathf.Abs( transform.position.x - nextPatrolPoint.position.x ); //Vector2.Distance(transform.position, nextPatrolPoint.position);
            Debug.Log(distanceToPatrolPoint);
            if (distanceToPatrolPoint < minDistanceToPatrolPoint)
            {
                return (int)AIStateType.Idle;
            }

            return (int)AIStateType.Patrol;

        }

        private Transform GetNextPatrolPoint()
        {
            float distanceToLeftPattrol = Vector2.Distance( transform.position , leftPatrolPoint.position );
            float distanceToRightPatrol = Vector2.Distance(transform.position, rightPatrolPoint.position);

            return distanceToRightPatrol > distanceToLeftPattrol ? rightPatrolPoint : leftPatrolPoint;
        }
    }

}

