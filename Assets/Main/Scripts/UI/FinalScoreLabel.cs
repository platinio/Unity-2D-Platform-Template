using UnityEngine;
using UnityEngine.UI;
using Platinio.TweenEngine;


namespace Gamaga.UI
{
    public class FinalScoreLabel : MonoBehaviour
    {
        [SerializeField] private Text label = null;
        [SerializeField] private float animTime = 0.0f;
        [SerializeField] private Ease ease = Ease.Linear;

        public void Show()
        {
            PlatinioTween.instance.ValueTween( 0.0f , ScoreManager.instance.Score , animTime ).SetEase(ease).SetOnUpdateFloat(
            delegate(float v) 
            {
                label.text = ((int)v).ToString();
            });
        }

    }

}

