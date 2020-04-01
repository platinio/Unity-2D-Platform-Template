using UnityEngine;

namespace Gamaga
{
    public class Actor : MonoBehaviour
    {
        private Rigidbody2D rb = null;
        private SpriteRenderer render = null;
        private Animator animator = null;

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

    }
}

