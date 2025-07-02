using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PauseManager
{
    private Image pausePanel;
    private Image[] pausePanelElements;
    
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
    }


    public void ResumeGameButton()
    {
        GameManager.Instance.AudioManager.PlayMusic("Game");
        isGamePaused = false;

        Color currentColor = pausePanel.color;
        currentColor.a = 0f;
        pausePanel.color = currentColor;

        foreach (var pauseElements in pausePanelElements)
        {
            Color currentColorElement = pauseElements.color;
            currentColorElement.a = 0f;
            pauseElements.color = currentColorElement;
        }

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
            GameManager.Instance.AudioManager.PlayMusic("Pause");
            GameManager.Instance.AudioManager.PlaySFX("ButtonClick");

            Color currentColor = pausePanel.color;
            currentColor.a = 181f / 255f;
            pausePanel.color = currentColor;

            foreach (var pauseElements in pausePanelElements)
            {
                Color currentColorElement = pauseElements.color;
                currentColorElement.a = 255/ 255f;
                pauseElements.color = currentColorElement;
            }

            isGamePaused = true;
            Time.timeScale = 0f;
            return;
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && isGamePaused)
        {
            GameManager.Instance.AudioManager.PlayMusic("Game");
            GameManager.Instance.AudioManager.PlaySFX("ButtonClick");

            Color currentColor = pausePanel.color;
            currentColor.a = 0f;
            pausePanel.color = currentColor;

            foreach (var pauseElements in pausePanelElements)
            {
                Color currentColorElement = pauseElements.color;
                currentColorElement.a = 0f;
                pauseElements.color = currentColorElement;
            }

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

        pausePanel = canvas.transform.Find("PausePanel").GetComponentInChildren<Image>();
        pausePanelElements = pausePanel.GetComponentsInChildren<Image>().Where(image => image != pausePanel).ToArray();
    }
}
