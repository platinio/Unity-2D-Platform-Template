using UnityEngine;

namespace Gamaga
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform target = null;
        [SerializeField] [Range(1.0f, 10.0f)] private float zoom = 5.0f;
        [SerializeField] [Range(0.1f, 2.0f)] private float followSmooth = 2.0f;
        [SerializeField] private Vector3 offset = Vector2.zero;
        [SerializeField] private float mirrorTime = 0.25f;

        private Vector2 velocity = Vector2.zero;
        private Camera thisCamera = null;
        private Vector2 lastTargetPostion = Vector2.zero;
        private Vector2 currentTargetDirection = Vector2.zero;
        private float mirrorTimer = 0.0f;
        private int cameraFacingDirection = 1;

        private void Awake()
        {
            thisCamera = GetComponent<Camera>();
        }

        private void LateUpdate()
        {
            if (GetTargetMovingDirection() != cameraFacingDirection)
            {
                mirrorTimer += Time.deltaTime;

                if (mirrorTimer > mirrorTime)
                {
                    MirrorCamera();
                    mirrorTimer = 0.0f;
                }
            }
            else
            {
                mirrorTimer = 0.0f;
            }

            Vector3 desirePosition = Vector2.SmoothDamp(transform.position, CalculateCameraDesirePosition(), ref velocity, followSmooth);
            desirePosition.z = transform.position.z;
            transform.position = desirePosition;
        }

        private Vector2 CalculateCameraDesirePosition()
        {
            return target.position + offset;
        }

        private int GetTargetMovingDirection()
        {
            Vector2 dir = new Vector2(target.transform.position.x, target.transform.position.y) - lastTargetPostion;
            dir.Normalize();

            return (int)Mathf.Sign(dir.x);
        }

        private void MirrorCamera()
        {
            offset.x *= -1;
            cameraFacingDirection *= -1;
        }
    }
}
