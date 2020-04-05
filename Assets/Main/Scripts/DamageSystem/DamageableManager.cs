using UnityEngine;
using UnityEngine.Events;
using Gamaga.Events;

namespace Gamaga.DamageSystem
{
    public class DamageableManager : MonoBehaviour, IDamageable
    {
        
        [SerializeField] private OnDamageEvent onDamage;
        [SerializeField] private UnityEvent onDead = null;
        [SerializeField] private float inmunityTime = 2.0f;

        private float inmunityTimer = 0.0f;

        public OnDamageEvent OnDamage { get { return onDamage; } }
        public UnityEvent OnDead { get { return onDead; } }
        public int HP { get; private set; }


        private void Update()
        {
            if (inmunityTimer > 0.0f)
                inmunityTimer -= Time.deltaTime;
        }

        public void DoDamage(DamageInfo info)
        {
            if (inmunityTimer > 0.0f)
                return;            
            inmunityTimer = inmunityTime;
            HP = Mathf.Max(HP - info.dmg , 0);
            onDamage.Invoke(info);

            if (HP <= 0)
            {
                onDead.Invoke();
            }
        }

        public void SetHP(int hp)
        {
            this.HP = hp;
        }

    }
}

