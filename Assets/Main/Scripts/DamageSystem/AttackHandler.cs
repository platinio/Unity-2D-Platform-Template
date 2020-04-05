using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamaga.DamageSystem
{
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


