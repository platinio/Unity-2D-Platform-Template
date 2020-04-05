using UnityEngine;

namespace Gamaga
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform target = null;
        [SerializeField] [Range(0.1f, 2.0f)] private float followSmooth = 2.0f;
        [SerializeField] private Vector3 offset = Vector2.zero;
        [SerializeField] private float mirrorTime = 0.25f;
        [SerializeField] private BoxCollider2D cameraLimit = null;


        private Vector2 minCameraposition = Vector2.zero;
        private Vector2 maxCameraPosition = Vector2.zero;
        private Vector2 velocity = Vector2.zero;
        private Vector2 lastTargetPostion = Vector2.zero;
        private float mirrorTimer = float.MinValue;
        private int cameraFacingDirection = 1;

        private void Awake()
        {
            Vector2 cameraLimitCenter = cameraLimit.transform.position;
            cameraLimitCenter += cameraLimit.offset;
            minCameraposition = new Vector2( cameraLimitCenter.x - ( cameraLimit.size.x / 2.0f ) , cameraLimitCenter.y - (cameraLimit.size.y / 2.0f));
            maxCameraPosition = new Vector2(cameraLimitCenter.x + (cameraLimit.size.x / 2.0f), cameraLimitCenter.y + (cameraLimit.size.y / 2.0f));
        }

        private void FixedUpdate()
        {
            MirrorUpdate();
            FollowTarget();
            ClampCameraPosition();
        }

        private void MirrorUpdate()
        {
            if ( ShouldMirrorCamera() )
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
        }

        private bool ShouldMirrorCamera()
        {
            int dir = GetTargetMovingDirection();
            
            if (dir == 0)
                return false;
           
           return dir != cameraFacingDirection;
        }

        private void FollowTarget()
        {
            Vector3 smoothPosition = CalculateCameraSmoothPosition();
            smoothPosition.z = transform.position.z;
            transform.position = smoothPosition;
        }

        private Vector2 CalculateCameraSmoothPosition()
        {
            return Vector2.SmoothDamp(transform.position, CalculateCameraDesirePosition(), ref velocity, followSmooth);
        }

        private Vector2 CalculateCameraDesirePosition()
        {
            return target.position + offset;
        }

        private int GetTargetMovingDirection()
        {
            Vector2 dir = new Vector2(target.transform.position.x, target.transform.position.y) - lastTargetPostion;

            if (Mathf.Abs( dir.x ) < 0.000001f)
                return 0;

            dir.Normalize();
            lastTargetPostion = target.transform.position;
            return (int)Mathf.Sign(dir.x);
        }

        private void MirrorCamera()
        {
            offset.x *= -1;
            cameraFacingDirection *= -1;
        }

        private void ClampCameraPosition()
        {
            Vector3 currentPosition = transform.position;

            if (currentPosition.y > maxCameraPosition.y)
            {
                currentPosition.y = maxCameraPosition.y;
            }

            else if (currentPosition.y < minCameraposition.y)
            {
                currentPosition.y = minCameraposition.y;
            }
            
            float horizontalTargetDiff = target.transform.position.x - transform.position.x;
            
            if (horizontalTargetDiff > maxCameraPosition.x)
            {
                currentPosition.x = target.transform.position.x - maxCameraPosition.x;
            }
            
            if (horizontalTargetDiff < minCameraposition.x)
            {
                currentPosition.x = target.transform.position.x - minCameraposition.x;
            }

            transform.position = currentPosition;

        }
    }
}
