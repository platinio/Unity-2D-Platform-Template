using UnityEngine;
using Gamaga.CharacterSystem;

namespace Gamaga
{
    [CreateAssetMenu(fileName = "GameSettings" , menuName = "Gamaga/Game Settings")]
    public class GameSettings : ScriptableObject
    {
        [SerializeField] private CharacterStats playerStats = null;
        [SerializeField] private CharacterStats enemyStats = null;

        public CharacterStats PlayerStats { get { return playerStats; } }
        public CharacterStats EnemyStats { get { return enemyStats; } }

    }
}

