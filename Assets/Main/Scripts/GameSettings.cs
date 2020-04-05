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

        public void SetDefaultValues()
        {
            if(playerStats != null)
                SetPlayerDefaultValues(playerStats);
            if(enemyStats != null)
                SetEnemyDefaultValues(enemyStats);
        }

        private void SetPlayerDefaultValues(CharacterStats stats)
        {
            stats.HP = 20;
            stats.Speed = 4;
            stats.JumpForce = 1500.0f;
            stats.MaxNumberOfJumps = 2;
        }

        private void SetEnemyDefaultValues(CharacterStats stats)
        {
            stats.HP = 1;
            stats.Speed = 1;
            stats.DMG = 1;
            stats.JumpForce = 0.0f;
            stats.MaxNumberOfJumps = 0;
        }


    }
}

