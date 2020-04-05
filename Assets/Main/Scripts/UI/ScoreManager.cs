using UnityEngine;
using UnityEngine.UI;
using Platinio.TweenEngine;
using Platinio;

namespace Gamaga.UI
{    
    public class ScoreManager : Singleton<ScoreManager>
    {
        [SerializeField] private Text scoreLabel = null;
        [SerializeField] private Text collectedItemAmountLabel = null;
        [SerializeField] private float animTime = 1.0f;
        [SerializeField] private int staticLength = 10;
        [SerializeField] private Ease ease = Ease.Linear;

        private int visualScore = 0;
        private int targetScore = 0;
        private int collectedItems = 0;

        private void Awake()
        {
            UpdateCollectedItemsAmountLabel();
        }

        public void GiveScore(int score)
        {
            targetScore += score;

            PlatinioTween.instance.ValueTween(visualScore , targetScore , animTime ).SetOnUpdateFloat( delegate (float v)
            {
                visualScore = (int)v;                
                scoreLabel.text = visualScore.ToString();
            } );
        }

        public void ItemCollected()
        {
            collectedItems++;
            UpdateCollectedItemsAmountLabel();
        }

        private void UpdateCollectedItemsAmountLabel()
        {            
            collectedItemAmountLabel.text = collectedItems.ToString();
        }
    }

}
