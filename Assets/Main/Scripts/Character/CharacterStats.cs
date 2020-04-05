using UnityEngine;

namespace Gamaga.CharacterSystem
{
    [CreateAssetMenu(fileName = "CharacterStats", menuName = "Gamaga/Character Stats")]
    public class CharacterStats : ScriptableObject
    {
        [SerializeField] private int hp = 20;
        [SerializeField] private int dmg = 1;
        [SerializeField] private float speed = 1.0f;
        [SerializeField] private float jumpForce = 1500.0f;
        [SerializeField] private int maxNumberOfJumps = 1;

        public int HP { get { return hp; } set { hp = value; } }
        public int DMG { get { return dmg; } set { dmg = value; } }
        public float Speed { get { return speed; } set { speed = value; } }
        public float JumpForce { get { return jumpForce; } set { jumpForce = value; } }
        public int MaxNumberOfJumps { get { return maxNumberOfJumps; } set { maxNumberOfJumps = value; } }

       
    }
}
