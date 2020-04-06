using UnityEngine;

namespace Gamaga
{
    public class ParentOnTriggerEnter : MonoBehaviour
    {
        [SerializeField] private Transform desireParent = null;

        private void OnTriggerEnter2D(Collider2D collider)
        {
            collider.transform.parent = desireParent;       
        }

        private void OnTriggerExit2D(Collider2D collider)
        {
            collider.transform.parent = null;
        }

    }

}
