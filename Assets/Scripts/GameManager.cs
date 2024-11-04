using UnityEngine;
using System;

public enum GameState
{
    Menu,
    Playing,
    Win,
    Lose,
    Pause,
    Credits
}

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get => instance; }

    private event Action gameStatePlayin;
    private event Action gameStateLose;
    private event Action gameStateWin;
    private event Action gameStatePause;

    private GameState gameState;
    public GameState GameState { get => gameState; }

    public Action GameStatePlaying {  get => gameStatePlayin; set => gameStatePlayin = value; }
    public Action GameStateLose { get => gameStateLose; set => gameStateLose = value; }
    public Action GameStateWin { get => gameStateWin; set => gameStateWin = value; }
    public Action GameStatePause { get => gameStatePause; set => gameStatePause = value; }


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
        CursorController(true);
    }


    void Update()
    {
        switch (gameState)
        {
            case GameState.Menu:
                CursorController(true); 
            break;

            case GameState.Playing:
                gameStatePlayin?.Invoke();
                CursorController(false);
            break;

            case GameState.Lose:
                gameStateLose?.Invoke();
                CursorController(true);
            break;

            case GameState.Win:
                gameStateWin?.Invoke();
                CursorController(true);
            break;

            case GameState.Credits:
                CursorController(true);
            break;

            case GameState.Pause:
                gameStatePause?.Invoke();
                CursorController(true);
            break;
        }
    }


    public void ChangeStateTo(GameState newState)
    {
        gameState = newState;
    }


    private void CursorController(bool cursorVisible)
    {
        Cursor.visible = cursorVisible;
    }
}
