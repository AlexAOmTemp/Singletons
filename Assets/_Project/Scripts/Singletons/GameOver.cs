using System.Collections;
using UnityEngine;

namespace Script
{
    public class GameOver : SingletonBase<GameOver>
    {
        [SerializeField] private GameObject gameOverCanvas;
        [SerializeField] private float delayForDeath = 2f;
        
        public void SetGameOver()
        {
            StartCoroutine(ShowDeathScreenAfterAnimation());
        }
        
        public void Die()
        {
            StartCoroutine(ShowDeathScreenAfterAnimation());
        }
        
        protected override void Awake()
        {
            base.Awake();
            //gameOverCanvas.SetActive(false);
        }
        
        private IEnumerator ShowDeathScreenAfterAnimation()
        {
            // Ждём окончания анимации
            yield return new WaitForSeconds(delayForDeath);
            
            gameOverCanvas.SetActive(true);

            Time.timeScale = 0f;
            Cursor.visible = true;
        }
    }
}