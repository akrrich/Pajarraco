using UnityEngine;
using System;

public enum GameState
{
    Playing,
    Win,
    Lose,
    Pause
}

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get => instance; }

    private event Action gameStatePlayin;
    private event Action gameStateLose;

    private GameState gameState;
    public GameState GameState { get => gameState; }

    public Action GameStatePlaying {  get => gameStatePlayin; set => gameStatePlayin = value; }
    public Action GameStateLose { get => gameStateLose; set => gameStateLose = value; }



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
        gameState = GameState.Playing;
    }


    void Update()
    {
        switch (gameState)
        {
            case GameState.Playing:
                gameStatePlayin?.Invoke();
            break;

            case GameState.Lose:
                gameStateLose?.Invoke();
            break;

            case GameState.Win: 

            break;

            case GameState.Pause: 

            break;
        }
    }


    public void ChangeStateTo(GameState newState)
    {
        gameState = newState;
    }
}
