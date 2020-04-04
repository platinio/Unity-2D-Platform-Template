using UnityEngine;
using UnityEngine.SceneManagement;
using Gamaga.UI;
using Platinio;

namespace Gamaga.GameLogic
{
    public class GameManager : Singleton<GameManager>
    {        
        [SerializeField] private SpriteRenderer playerSR = null;
        [SerializeField] private SpriteRenderer gameOverSR = null;
        [SerializeField] private Popup gameOverPopup = null;

        private const string GameOverLayerName = "GameOver";


        private void Awake()
        {
            Time.timeScale = 1.0f;
        }

        public void GameOver()
        {
            gameOverSR.Fade(0.9f , 2.0f).SetEase(Ease.EaseOutExpo).SetEvent(delegate 
            {                
                playerSR.sortingLayerName = GameOverLayerName;                
            } , 0.5f ).SetDelay(0.25f).SetOnComplete(
            delegate            
            {
                gameOverPopup.OnClick.AddListener( ReloadCurrentScene );
                gameOverPopup.Show( delegate 
                {
                    Time.timeScale = 0.0f;
                } );
            });
        }

        private void ReloadCurrentScene()
        {
            SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex );
        }

    }

}

