using UnityEngine;
using UnityEngine.Events;
using Gamaga.Events;

namespace Gamaga.DamageSystem
{
    public class DamageableManager : MonoBehaviour, IDamageable
    {
        [SerializeField] private int hp = 0;
        [SerializeField] private OnDamageEvent onDamage;
        [SerializeField] private UnityEvent onDead = null;
        

        public OnDamageEvent OnDamage { get { return onDamage; } }
        public UnityEvent OnDead { get { return onDead; } }
        public int HP { get { return hp; } }


        public void DoDamage(DamageInfo info)
        {
            hp = Mathf.Max( hp - info.dmg , 0);
            onDamage.Invoke(info);

            if (hp <= 0)
            {
                onDead.Invoke();
            }
        }

        public void SetHP(int hp)
        {
            this.hp = hp;
        }

    }
}

