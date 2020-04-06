using UnityEngine;

namespace Gamaga.DamageSystem
{
    /// <summary>
    /// This class just handle the enable or disable or an attack zone mostly for physical attacks
    /// </summary>
    public class AttackHandler : MonoBehaviour
    {
        [SerializeField] private DamageZone damageZone = null;

        public void EnableDamageZone()
        {
            damageZone.gameObject.SetActive(true);
        }

        public void DisableDamageZone()
        {
            damageZone.gameObject.SetActive(false);
        }

    }

}


