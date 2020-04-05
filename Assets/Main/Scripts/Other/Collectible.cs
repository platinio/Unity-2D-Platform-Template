using UnityEngine;
using UnityEngine.Events;
using Gamaga.UI;

namespace Gamaga
{    
    public class Collectible : MonoBehaviour
    {
        [SerializeField] private float verticalMovementAmount = 0.5f;
        [SerializeField] private float animTime = 1.0f;
        [SerializeField] private UnityEvent onCollect = null;
        [SerializeField] private int score = 100;

        private SpriteRenderer render = null;
        private bool canCollect = true;

        private void Awake()
        {
            GetComponent<Collider2D>().isTrigger = true;
            render = GetComponent<SpriteRenderer>();
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (!canCollect)
                return;
            
            transform.Move( transform.position + ( Vector3.up * verticalMovementAmount ) , animTime );
            render.FadeOut( animTime * 1.3f ).SetEase(Ease.EaseOutExpo).SetOnComplete( delegate { Destroy(gameObject); } );
            canCollect = false;
            onCollect.Invoke();
            ScoreManager.instance.GiveScore(score);
            ScoreManager.instance.ItemCollected();
        }

    }

}
