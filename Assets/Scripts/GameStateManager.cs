using System.Collections;
using UnityEngine;

public enum GameState
{
    MainMenu,
    Playing,
    Pause,
    GameOver
}

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;

    public GameObject mainMenuUI;
    public GameObject inGameMenuUI;
    public GameObject PauseMenuUI;
    public GameObject GameOverUI;

    [Header("New UI")]
    public GameObject shopMenuUI;

    public GameState currentState { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ChangeState(GameState.MainMenu);
    }

    public void ChangeState(GameState newState)
    {
        StopAllCoroutines();

        currentState = newState;

        StartCoroutine(TransitionToState(newState));
    }

    public void ChangeToMainMenu()
    {
        ChangeState(GameState.MainMenu);
    }

    public void ChangeToPlaying()
    {
        ChangeState(GameState.Playing);
    }

    public void ChangeToPause()
    {
        ChangeState(GameState.Pause);
    }

    public void ChangeToGameOver()
    {
        ChangeState(GameState.GameOver);
    }

    public void OpenShop()
    {
        HideAllMenu();

        if (shopMenuUI != null)
        {
            shopMenuUI.SetActive(true);
        }

        BallColorShop shop = FindFirstObjectByType<BallColorShop>();

        if (shop != null)
        {
            shop.UpdateShopUI();
        }

        Time.timeScale = 0;
    }

    public void CloseShop()
    {
        HideAllMenu();

        if (mainMenuUI != null)
        {
            mainMenuUI.SetActive(true);
        }

        Time.timeScale = 0;
    }

    private IEnumerator TransitionToState(GameState newState)
    {
        if (newState == GameState.Playing)
        {
            yield return new WaitForSecondsRealtime(0.2f);
        }

        currentState = newState;
        HandleStateChange();
    }

    private void HandleStateChange()
    {
        HideAllMenu();

        switch (currentState)
        {
            case GameState.MainMenu:
                Time.timeScale = 0;
                mainMenuUI.SetActive(true);
                AudioManager.instance.PlayMusic(AudioManager.instance.menuMusic);
                break;

            case GameState.Playing:
                inGameMenuUI.SetActive(true);
                Time.timeScale = 1;
                AudioManager.instance.PlayMusic(AudioManager.instance.inGameMusic);
                break;

            case GameState.Pause:
                PauseMenuUI.SetActive(true);
                Time.timeScale = 0;
                AudioManager.instance.PlayMusic(AudioManager.instance.menuMusic);
                break;

            case GameState.GameOver:
                Time.timeScale = 0;

                GameOverUI.SetActive(true);

                if (ScoreManager.instance != null)
                {
                    ScoreManager.instance.UpdateFinalScore();
                }

                AudioManager.instance.PlayMusic(AudioManager.instance.menuMusic);
                break;
        }
    }

    private void HideAllMenu()
    {
        if (mainMenuUI != null)
        {
            mainMenuUI.SetActive(false);
        }

        if (inGameMenuUI != null)
        {
            inGameMenuUI.SetActive(false);
        }

        if (PauseMenuUI != null)
        {
            PauseMenuUI.SetActive(false);
        }

        if (GameOverUI != null)
        {
            GameOverUI.SetActive(false);
        }

        if (shopMenuUI != null)
        {
            shopMenuUI.SetActive(false);
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1;

        GameResetManager resetManager = FindFirstObjectByType<GameResetManager>();

        if (resetManager != null)
        {
            resetManager.ResetGameObjects();
        }

        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.ResetScore();
        }

        ChangeState(GameState.Playing);
    }
}
