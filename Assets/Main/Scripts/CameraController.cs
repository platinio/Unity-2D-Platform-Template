using UnityEngine;

namespace Gamaga
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform target = null;
        [SerializeField] [Range(1.0f, 10.0f)] private float zoom = 5.0f;
        [SerializeField] [Range(0.1f, 5.0f)] private float followSmooth = 2.0f;
        [SerializeField] private Vector3 offset = Vector2.zero;

        private Vector2 velocity = Vector2.zero;
        private Camera thisCamera = null;

        private void Awake()
        {
            thisCamera = GetComponent<Camera>();
        }

        private void LateUpdate()
        {
            Vector3 desirePosition = Vector2.SmoothDamp(transform.position, CalculateCameraDesirePosition(), ref velocity, followSmooth);
            desirePosition.z = transform.position.z;
            transform.position = desirePosition;
        }

        private Vector2 CalculateCameraDesirePosition()
        {
            return target.position + offset;
        }

    }
}
