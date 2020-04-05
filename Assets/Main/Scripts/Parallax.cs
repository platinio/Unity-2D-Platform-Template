using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamaga
{
    public class Parallax : MonoBehaviour
    {
        [SerializeField] private float parallaxSpeed = 0.0f;
       
        
        private float startPosition = 0.0f;
        private Transform mainCamera = null;        
        private float size = 0.0f;       
        private float y;
        private SpriteRenderer render = null;
        [SerializeField] private GameObject left = null;
        [SerializeField] private GameObject center = null;
        [SerializeField] private GameObject right = null;

        private void Awake()
        {
            render = GetComponent<SpriteRenderer>();
            mainCamera = Camera.main.transform;
            startPosition = transform.position.x;           
            y = transform.position.y;
            size = render.bounds.size.x;
            center = gameObject;
            
            left = CreateParallax(transform.position + (Vector3.left * size));
            right = CreateParallax(transform.position + (Vector3.right * size));
        }

        private void LateUpdate()
        {
            float d = (mainCamera.position.x * parallaxSpeed);
            transform.position = new Vector3( startPosition + d , y , transform.position.z );

            d = Mathf.Abs(center.transform.position.x - mainCamera.position.x );
           // Debug.Log( d + " " + size * 0.5f );
            if (d > size * 0.4f)
            {
                float dir = Mathf.Sign( mainCamera.transform.position.x - center.transform.position.x );

                startPosition += size * dir;


            }


        }

        private GameObject CreateParallax(Vector3 position)
        {
            GameObject go = new GameObject("Parallax" , typeof(SpriteRenderer) );
            go.transform.parent = transform;
            go.transform.position = position;
            SpriteRenderer r = go.GetComponent<SpriteRenderer>();
            r.sprite = render.sprite;
            r.sortingLayerName = render.sortingLayerName;
            r.sortingOrder = render.sortingOrder;

            go.transform.localScale = Vector3.one;

            return go;
        }


    }
}
