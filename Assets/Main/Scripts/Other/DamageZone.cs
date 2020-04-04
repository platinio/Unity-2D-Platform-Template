using UnityEngine;
using Gamaga.DamageSystem;

namespace Gamaga
{
    public class DamageZone : MonoBehaviour
    {
        [SerializeField] private int damage = 1;
        [SerializeField] private float pushForce = 50.0f;
        [SerializeField] private ForceMode2D forceMode = ForceMode2D.Impulse;

        private DamageInfo damageInfo;

        private void Awake()
        {
            damageInfo.dmg = damage;
            damageInfo.force = pushForce;
            damageInfo.forceMode = forceMode;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            IDamageable damageable = col.GetComponent<IDamageable>();
            
            if (damageable != null)
            {                
                Vector2 thisPos = transform.position;
                Vector2 otherPos = col.transform.position;
                damageInfo.dir = (otherPos - thisPos).normalized;
                damageable.DoDamage( damageInfo );
            }

        }

    }

}

