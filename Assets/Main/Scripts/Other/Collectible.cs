using UnityEngine;
using UnityEngine.Events;

namespace Gamaga
{    
    public class Collectible : MonoBehaviour
    {
        [SerializeField] private float verticalMovementAmount = 0.5f;
        [SerializeField] private float animTime = 1.0f;
        [SerializeField] private UnityEvent onCollect = null;

        private SpriteRenderer render = null;
        private bool canCollect = true;

        private void Awake()
        {
            GetComponent<Collider2D>().isTrigger = true;
            render = GetComponent<SpriteRenderer>();
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            Debug.Log(collider.name);
            if (!canCollect)
                return;
            
            transform.Move( transform.position + ( Vector3.up * verticalMovementAmount ) , animTime );
            render.FadeOut( animTime * 1.3f ).SetEase(Ease.EaseOutExpo).SetOnComplete( delegate { Destroy(gameObject); } );
            canCollect = false;
            onCollect.Invoke();
        }

    }

}
