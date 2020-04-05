
using UnityEngine;

namespace Gamaga
{
    public class Parallax : MonoBehaviour
    {
        [SerializeField] private float parallaxSpeed = 0.0f;

        private float spriteSize = 0.0f;
        private Vector2 startPosition = Vector2.zero;       
        private Transform mainCamera = null;  
        private SpriteRenderer render = null;

        public bool debugModeOn = false;

        private void Awake()
        {
            Initialize();

            
            CreateSpriteAtPosition(transform.position + (Vector3.left * spriteSize));
            CreateSpriteAtPosition(transform.position + (Vector3.right * spriteSize));
        }

        private void Initialize()
        {
            render = GetComponent<SpriteRenderer>();
            mainCamera = Camera.main.transform;
            startPosition.x = transform.position.x;
            startPosition.y = transform.position.y;
            spriteSize = render.bounds.size.x;            
        }


        private void LateUpdate()
        {
            UpdateParallaxEffect();
            CheckParallaxBounds();
        }

        private void UpdateParallaxEffect()
        {
            float parallax = (mainCamera.position.x * parallaxSpeed);
            transform.position = new Vector3(startPosition.x + parallax, transform.position.y, transform.position.z);            
        }

        private void CheckParallaxBounds()
        {
            float d = Mathf.Abs(transform.position.x - mainCamera.position.x);
            
            if (d > spriteSize * 0.6f)
            {
                float dir = Mathf.Sign(mainCamera.transform.position.x - transform.position.x);
                startPosition.x += spriteSize * dir;
            }
        }


        private GameObject CreateSpriteAtPosition(Vector3 position)
        {
            GameObject go = new GameObject("Parallax" , typeof(SpriteRenderer) );
            go.transform.parent = transform;
            go.transform.position = position;
            go.transform.localScale = Vector3.one;

            SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
            CopySpriteRenderSettings( render , sr );

            return go;
        }

        private void CopySpriteRenderSettings(SpriteRenderer from , SpriteRenderer to)
        {            
            to.sprite = from.sprite;
            to.sortingLayerName = from.sortingLayerName;
            to.sortingOrder = from.sortingOrder;
        }


    }
}
