using System.Collections;
using UnityEngine;

public class PauseManager
{
    private GameObject pausePanel;
    
    private bool isGamePaused = false;

    public bool IsGamePaused { get => isGamePaused; set => isGamePaused = value; }


    public PauseManager()
    {
        SuscribeToUpdateManagerEvent();
        SuscribeToSelectorMainMenuEvent();
    }


    // Simulacion de Update
    void UpdatePauseManager()
    {
        if (pausePanel != null)
        {
            CheckPauseStatus();
        }

        Debug.Log(isGamePaused);
    }


    public void ResumeGameButton()
    {
        isGamePaused = false;
        pausePanel?.SetActive(false);
        Time.timeScale = 1f;
    }


    private void SuscribeToUpdateManagerEvent()
    {
        GameManager.Instance.UpdateManager.OnUpdate += UpdatePauseManager;
    }

    private void SuscribeToSelectorMainMenuEvent()
    {
        SelectorMainMenu.OnPausePanelFind += FindPanelInHierarchy;
    }

    private void CheckPauseStatus()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isGamePaused)
        {
            GameManager.Instance.AudioManager.PlaySFX("ButtonClick");

            pausePanel?.SetActive(true);
            isGamePaused = true;
            Time.timeScale = 0f;
            return;
        }

        else if (Input.GetKeyDown(KeyCode.T) && isGamePaused)
        {
            GameManager.Instance.AudioManager.PlaySFX("ButtonClick");

            pausePanel?.SetActive(false);
            isGamePaused = false;
            Time.timeScale = 1f;
            return;
        }
    }

    private void FindPanelInHierarchy()
    {
        // Se usa el GameManager como instancia porque hereda de Monobehaviour
        GameManager.Instance.StartCoroutine(FindPausePanel());
    }

    private IEnumerator FindPausePanel()
    {
        yield return new WaitForSeconds(1);

        GameObject canvas = GameObject.Find("CanvasPause");

        pausePanel = canvas.transform.Find("PausePanel").gameObject;
    }
}
