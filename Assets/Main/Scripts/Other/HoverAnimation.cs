using UnityEngine;

namespace Gamaga
{
    public class HoverAnimation : MonoBehaviour
    {
        [SerializeField] [Range(0.01f, 0.5f)] private float range = 0.25f;
        [SerializeField] [Range(0.01f, 2.0f)] private float time = 0.0f;
        [SerializeField] private Ease ease = Ease.Linear;

        private bool goingUp = true;

        private void Awake()
        {
            Hover();
        }

        private void Hover()
        {
            goingUp = !goingUp;
            Vector2 targetPosition = GetHoverPosition();

            transform.Move(targetPosition, time).SetOnComplete(Hover).SetEase(ease).SetOwner(gameObject);
        }

        private Vector3 GetHoverPosition()
        {
            return new Vector3( transform.position.x , transform.position.y + ( Random.Range( range / 2 , range ) * (goingUp ? 1 : -1) ) , transform.position.z );
        }

        public void Stop()
        {
            gameObject.CancelAllTweens();
        }

    }

}
