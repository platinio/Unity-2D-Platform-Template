using UnityEngine;

namespace Gamaga
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer playerSR = null;
        [SerializeField] private SpriteRenderer gameOverSR = null;

        private const string GameOverLayerName = "GameOver";

        public void GameOver()
        {
            gameOverSR.Fade(0.9f , 2.0f).SetEase(Ease.EaseOutExpo).SetEvent(delegate 
            {                
                playerSR.sortingLayerName = GameOverLayerName;
                playerSR.sortingLayerID = 1;
            } , 0.5f ).SetDelay(2.0f).SetOnComplete(delegate { Time.timeScale = 0.0f; });
        }
    }

}

