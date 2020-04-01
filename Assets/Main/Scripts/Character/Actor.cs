using UnityEngine;

namespace Gamaga
{
    public class Actor : MonoBehaviour
    {
        [SerializeField] protected BoxCollider2D groundedCollider = null;

        protected Rigidbody2D rb = null;
        protected SpriteRenderer render = null;
        protected Animator animator = null;

        private void Awake()
        {
            GetReferences();
        }

        private void GetReferences()
        {
            rb = GetComponent<Rigidbody2D>();
            render = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
        }

        private void Initialize()
        {
            groundedCollider.enabled = false;
        }

    }
}

