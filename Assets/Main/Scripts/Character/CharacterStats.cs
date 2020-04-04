using UnityEngine;

namespace Gamaga.CharacterSystem
{
    [CreateAssetMenu(fileName = "CharacterStats" , menuName = "Gamaga/Character Stats")]
    public class CharacterStats : ScriptableObject
    {
        [SerializeField] private int hp = 20;
        [SerializeField] private int dmg = 1;
        [SerializeField] private float speed = 1.0f;
        [SerializeField] private float jumpForce = 1500.0f;

        public int HP { get { return hp; } }
        public int DMG { get { return dmg; } }
        public float Speed { get { return speed; } }

        public float JumpForce { get { return jumpForce; } }

    }
}
