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
        [SerializeField] private Popup levelCompletePopup = null;
        [SerializeField] private GameObject mobileInputCanvas = null;

        private const string GameOverLayerName = "GameOver";


        protected override void Awake()
        {
            //spawn virtual control for android devices
            #if UNITY_ANDROID && !UNITY_EDITOR
            Instantiate(mobileInputCanvas);
            #endif

            base.Awake();
            Time.timeScale = 1.0f;
        }

        public void GameOver()
        {
            //here I am using my tween library you can read more about it here https://github.com/platinio/PlatinioTween
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

        public void LevelComplete()
        {
            //here I am using my tween library you can read more about it here https://github.com/platinio/PlatinioTween
            gameOverSR.Fade(0.9f, 2.0f).SetEase(Ease.EaseOutExpo).SetEvent(delegate
            {
                playerSR.sortingLayerName = GameOverLayerName;
            }, 0.5f).SetDelay(0.25f).SetOnComplete(
            delegate
            {
                levelCompletePopup.OnClick.AddListener(ReloadCurrentScene);
                levelCompletePopup.Show();
            });
        }

        private void ReloadCurrentScene()
        {
            SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex );
        }

    }

}

