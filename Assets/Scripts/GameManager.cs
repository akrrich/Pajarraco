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

    private UpdateManager updateManager = new UpdateManager();


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
    }


    void Update()
    {
        updateManager.UpdateAllGame(gameState);
    }


    public void ChangeStateTo(GameState newState)
    {
        gameState = newState;
    }
}
