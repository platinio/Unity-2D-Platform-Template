using UnityEngine;
using Gamaga.Events;

namespace Gamaga.DamageSystem
{
    public class DamageableManager : MonoBehaviour, IDamageable
    {
        [SerializeField] private int hp = 0;
        [SerializeField] private OnValueChangeEvent onHpChange;

        public OnValueChangeEvent OnHPChange { get { return onHpChange; } }
        public int HP { get { return hp; } }

        private void Start()
        {
            onHpChange.Invoke( hp );
        }

        public void DoDamage(DamageInfo info)
        {
            hp -= info.dmg;
            onHpChange.Invoke(hp);
        }
    }
}

