using UnityEngine;
using Gamaga.DamageSystem;

namespace Gamaga.UI
{
    public class HeartPiecesLifeBar : MonoBehaviour
    {
        [SerializeField] private DamageableManager damageableManager = null;
        [SerializeField] private Heart heartPrefab = null;
        [SerializeField] private Transform heartContainer = null;
        
        private Heart[] heartArray = null;
        private int heartIndex = 0;

        private void Awake()
        {
            CreateHearts();
            damageableManager.OnDamage.AddListener(delegate (DamageInfo info){ DecreaseValue(info.dmg); });
        }

        private void CreateHearts()
        {
            int heartAmount = Mathf.CeilToInt( (float)damageableManager.HP / (float)Heart.MAX_PIECES );
            heartArray = new Heart[heartAmount];
            heartIndex = heartArray.Length - 1;

            for (int n = 0; n < heartAmount; n++)
            {
                heartArray[n] = InstantiateHeart();
            }

            if (damageableManager.HP % Heart.MAX_PIECES != 0)
            {
                heartArray[heartIndex].SetValue(damageableManager.HP % Heart.MAX_PIECES);
            }
            

        }

        private Heart InstantiateHeart()
        {
            Heart heart = Instantiate(heartPrefab);
            heart.transform.parent = heartContainer;
            heart.SetValue( Heart.MAX_PIECES );

            return heart;
        }

        private void DecreaseValue(int value)
        {
            if (heartIndex < 0)
                return;

            int result = heartArray[heartIndex].CurrentValue - value;

            if (result < 0)
            {               
                heartArray[heartIndex].SetValue(0);
                heartIndex--;
                DecreaseValue(Mathf.Abs(result));
            }
            else
            {
                heartArray[heartIndex].SetValue(result);
            }
        }



    }

}
