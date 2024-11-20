using UnityEngine;

public class UpdateManager
{
    public void UpdateAllGame(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Menu:
                GameManager.Instance.GameStateMenu?.Invoke();
                break;

            case GameState.Playing:
                GameManager.Instance.GameStatePlaying?.Invoke();
                break;

            case GameState.Defeated:
                GameManager.Instance.GameStateDefeated?.Invoke();
                break;

            case GameState.TotalDefeated:
                GameManager.Instance.GameStateTotalDefeated?.Invoke();
                break;

            case GameState.Win:
                GameManager.Instance.GameStateWin?.Invoke();
                break;

            case GameState.Credits:
                GameManager.Instance.GameStateCredits?.Invoke();
                break;

            case GameState.Pause:
                GameManager.Instance.GameStatePause?.Invoke();
                break;
        }

        CursorController(gameState);
        TimeScaleMode(gameState);
    }


    private void CursorController(GameState gameState)
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

    private void TimeScaleMode(GameState gameState)
    {
        if (gameState != GameState.Defeated && gameState != GameState.Pause)
        {
            Time.timeScale = 1f;
        }

        else if (gameState == GameState.Defeated || gameState == GameState.Pause)
        {
            Time.timeScale = 0f;
        }
    }
}
