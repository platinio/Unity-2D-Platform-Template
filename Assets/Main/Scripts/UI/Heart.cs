using UnityEngine;
using UnityEngine.UI;
using Platinio.TweenEngine;

namespace Gamaga.UI
{
    public class Heart : MonoBehaviour
    {
        [SerializeField] private Image heartImg = null;
        [SerializeField] private float animationTime = 0.5f;
        [SerializeField] private Ease ease = Ease.Linear;

        public int CurrentValue 
        {
            get 
            {
                return (int)(heartImg.fillAmount * MAX_PIECES); 
            }
        }

        public const int MAX_PIECES = 4;

        public void SetValue(int value)
        {
            PlatinioTween.instance.CancelTween(gameObject);

            float targetFillValue = (float)value / (float)MAX_PIECES;
            PlatinioTween.instance.ValueTween( heartImg.fillAmount , targetFillValue , animationTime ).SetEase(ease).SetOnUpdateFloat(delegate (float v)
            {
                heartImg.fillAmount = v;
            }).SetOwner(gameObject);

            
        }

    }

}
