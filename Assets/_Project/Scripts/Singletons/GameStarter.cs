using System;
using Cysharp.Threading.Tasks;
using Script;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public sealed class GameStarter : SingletonBase<GameStarter>
{
    [Header("Settings")]
    [SerializeField] private float restartDelay = 3f;
    
    [Header("References")]
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject backgroundMusic;

    [Header("UI Settings")]
    [SerializeField] private GameObject menuUI;
    [SerializeField] private GameObject coinsObject;
    [SerializeField] private GameObject lifeObject;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject recordObject;
    
    public event Action OnGameStarted; // Событие при старте игры
    public event Action OnGameOver;   // Событие при проигрыше
    
    private bool isGameStarted = false;

 
    public void GameOverPlayer()
    {
        isGameStarted = false;
        gameOverUI.SetActive(true);
        OnGameOver?.Invoke();

        Invoke(nameof(RestartGame), restartDelay);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void StartGame()
    {
        isGameStarted = true;
        backgroundMusic.SetActive(true);
        Time.timeScale = 1f;
        OnGameStarted?.Invoke();
        SetUiActive();
        GameStateManager.Instance.StartGame().Forget();
    }

    private void Start()
    {
        Time.timeScale = 0f;
        StartGame();
    }
    
    private void SetUiActive()
    {
        if (menuUI != null)
            menuUI.SetActive(false);
        if (gameOver != null)
            gameOver.SetActive(false);
        if (recordObject != null)
            recordObject.SetActive(true);
        if (coinsObject != null)
            coinsObject.SetActive(true);
        if (lifeObject != null)
            lifeObject.SetActive(true);
    }
}