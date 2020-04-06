using UnityEngine;
using Gamaga.GameLogic;

namespace Gamaga
{
    public class LevelCompleteTrigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collider)
        {
            GameManager.instance.LevelComplete();

            PlayerController pc = collider.GetComponent<PlayerController>();

            if (pc != null)
            {
                pc.enabled = false;
            }

        }
    }

}
