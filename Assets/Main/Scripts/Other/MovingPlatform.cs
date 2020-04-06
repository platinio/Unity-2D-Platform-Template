using UnityEngine;

namespace Gamaga
{
    public class MovingPlatform : MonoBehaviour
    {
        [SerializeField] private Transform start = null;
        [SerializeField] private Transform end = null;
        [SerializeField] private float speed = 0.2f;
        [SerializeField] private float minDistance = 0.25f;

        private Transform target = null;
      

        private void Awake()
        {            
            target = start;
        }

        private void Update()
        {
            Vector3 dir = target.transform.position - transform.position;
            
            transform.position += dir.normalized * speed * Time.deltaTime;
            
            float distance = Vector2.Distance(transform.position, target.position);

            if (distance < minDistance)
            {
                ChangeTarget();
            }
        }

        private void ChangeTarget()
        {
            target = target == start ? end : start;
        }

    }

}

