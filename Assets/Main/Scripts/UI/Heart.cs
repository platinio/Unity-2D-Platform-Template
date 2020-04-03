using UnityEngine;
using UnityEngine.UI;

namespace Gamaga.UI
{
    public class Heart : MonoBehaviour
    {
        [SerializeField] private Image heartImg = null;

        public int CurrentValue 
        {
            get 
            {
                return Mathf.RoundToInt( heartImg.fillAmount ) * MAX_PIECES; 
            }
        }

        public const int MAX_PIECES = 4;

        public void SetValue(int value)
        {
            heartImg.fillAmount = (float) value / (float) MAX_PIECES;
            Debug.Log((float)value / (float)MAX_PIECES);
        }

    }

}
