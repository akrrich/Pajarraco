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

    [SerializeField] private Texture2D customCursorTexture;
    private Texture2D defaultCursorTexture;

    private Vector2 cursorHotsSpot;

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

    public Texture2D CustomCursorTexture { get => customCursorTexture; set => customCursorTexture = value; }
    public Texture2D DefaultCursorTexture { get => defaultCursorTexture; set => defaultCursorTexture = value; }
    public Vector2 CursorHotsSpot { get => cursorHotsSpot; }


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
        cursorHotsSpot = new Vector2(Instance.CustomCursorTexture.width / 2, Instance.CustomCursorTexture.height / 2);
        gameState = GameState.Menu;
        CursorController(true);
    }


    void Update()
    {
        print(gameState);

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
