using UnityEngine;
using Gamaga.Events;
using Gamaga.CharacterSystem;

namespace Gamaga.DamageSystem
{
    public class DamageableManager : MonoBehaviour, IDamageable
    {
        [SerializeField] private int hp = 0;
        [SerializeField] private OnDamageEvent onDamage;
        [SerializeField] private Rigidbody2D rb = null;

        public OnDamageEvent OnDamage { get { return onDamage; } }
        public int HP { get { return hp; } }


        public void DoDamage(DamageInfo info)
        {

            Character c = GetComponent<Character>();
            c.HandleHit( info.dir * info.force , info.forceMode );
            //Debug.Log(info.force * info.dir);
            //rb.AddForce( info.force * Vector2.right , info.forceMode );
            hp -= info.dmg;
            onDamage.Invoke(info);
        }
    }
}

