using UnityEngine;
using Gamaga.Events;
using Gamaga.CharacterSystem;

namespace Gamaga.DamageSystem
{
    public class DamageableManager : MonoBehaviour, IDamageable
    {
        [SerializeField] private int hp = 0;
        [SerializeField] private OnDamageEvent onDamage;
        

        public OnDamageEvent OnDamage { get { return onDamage; } }
        public int HP { get { return hp; } }


        public void DoDamage(DamageInfo info)
        {

            hp -= info.dmg;
            onDamage.Invoke(info);
        }
    }
}

