using UnityEngine;
using System;

public enum GameState
{
    Menu,
    Playing,
    Win,
    Defeated,
    TotalDefeated,
    Pause,
    Credits
}

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get => instance; }

    private event Action gameStateMenu;
    private event Action gameStatePlayin;
    private event Action gameStateDefeated;
    private event Action gameStateTotalDefeated;
    private event Action gameStateWin;
    private event Action gameStatePause;
    private event Action gameStateCredits;

    private GameState gameState;
    public GameState GameState { get => gameState; }

    public Action GameStateMenu { get => gameStateMenu; set => gameStateMenu = value; }
    public Action GameStatePlaying {  get => gameStatePlayin; set => gameStatePlayin = value; }
    public Action GameStateDefeated { get => gameStateDefeated; set => gameStateDefeated = value; }
    public Action GameStateTotalDefeated { get => gameStateTotalDefeated; set => gameStateTotalDefeated = value; }
    public Action GameStateWin { get => gameStateWin; set => gameStateWin = value; }
    public Action GameStatePause { get => gameStatePause; set => gameStatePause = value; }
    public Action GameStateCredits { get => gameStateCredits; set => gameStateCredits = value; }


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        gameState = GameState.Menu;
        Cursor.visible = true;
    }


    void Update()
    {
        CheckCurrentState();

        TimeScaleMode();
        CursorController();
    }


    public void ChangeStateTo(GameState newState)
    {
        gameState = newState;
    }


    private void CheckCurrentState()
    {
        switch (gameState)
        {
            case GameState.Menu:
                gameStateMenu?.Invoke();
                break;

            case GameState.Playing:
                gameStatePlayin?.Invoke();
                break;

            case GameState.Defeated:
                gameStateDefeated?.Invoke();
                break;

            case GameState.TotalDefeated:
                gameStateTotalDefeated?.Invoke();
                break;

            case GameState.Win:
                gameStateWin?.Invoke();
                break;

            case GameState.Credits:
                gameStateCredits?.Invoke();
                break;

            case GameState.Pause:
                gameStatePause?.Invoke();
                break;
        }
    }

    private void CursorController()
    {
        if (gameState != GameState.Playing)
        {
            Cursor.visible = true;
        }

        else
        {
            Cursor.visible = false;
        }
    }

    // Metodo provisorio
    private void TimeScaleMode()
    {
        if (gameState != GameState.Defeated)
        {
            Time.timeScale = 1f;
        }

        else
        {
            Time.timeScale = 0f;
        }
    }
}
