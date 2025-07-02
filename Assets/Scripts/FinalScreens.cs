using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FinalScreens : MonoBehaviour
{
    private Image winPanel;
    private Image loosePanel;


    void Awake()
    {
        SuscriptionEvents();
        StartCoroutine(GetComponents());
    }

    // Simulacion de Update
    void UpdateFinalScreens()
    {
        UpdateLoosePanelInfo();
        UpdateWinPanelInfo();
    }

    void OnDestroy()
    {
        UnsuscriptionEvents();
    }


    private void SuscriptionEvents()
    {
        GameManager.Instance.UpdateManager.OnUpdate += UpdateFinalScreens;
        PlayerView.OnPlayerDeath += ShowLoosePanel;
        EnemyView.OnEnemyDeath += ShowWinPanel;
    }

    private void UnsuscriptionEvents()
    {
        GameManager.Instance.UpdateManager.OnUpdate -= UpdateFinalScreens;
        PlayerView.OnPlayerDeath -= ShowLoosePanel;
        EnemyView.OnEnemyDeath -= ShowWinPanel;
    }

    private IEnumerator GetComponents()
    {
        yield return new WaitForSeconds(1f);

        winPanel = GameObject.Find("CanvasWinScreen").transform.Find("WinPanel").GetComponent<Image>();
        loosePanel = GameObject.Find("CanvasLooseScreen").transform.Find("LoosePanel").GetComponent<Image>();
    }

    private void ShowLoosePanel()
    {
        Time.timeScale = 0f;

        Color currentColor = loosePanel.color;
        currentColor.a = 255 / 255f;
        loosePanel.color = currentColor;
    }

    private void ShowWinPanel()
    {
        Time.timeScale = 0f;
        
        Color currentColor = winPanel.color;
        currentColor.a = 255 / 255f;
        winPanel.color = currentColor;
    }

    private void UpdateLoosePanelInfo()
    {
        if (loosePanel != null)
        {
            if (loosePanel.color.a == 1f)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    GameManager.Instance.AudioManager.PlaySFX("ButtonClick");

                    Time.timeScale = 1f;
                    GameManager.Instance.ScenesManager.ChangeScene("Level", "LevelUI");
                    GameManager.Instance.PoolerManager.ReturnAllBulletsToPool();
                    GameManager.Instance.PoolerManager.ReinitializeBulletsReferences();
                    SelectorMainMenu.OnPausePanelFind?.Invoke();
                }

                if (Input.GetKeyDown(KeyCode.M))
                {
                    GameManager.Instance.AudioManager.PlaySFX("ButtonClick");

                    Time.timeScale = 1f;
                    GameManager.Instance.ScenesManager.ChangeScene("MainMenu", "MainMenuUI");
                    GameManager.Instance.PoolerManager.ReturnAllBulletsToPool();
                }
            }
        }
    }

    private void UpdateWinPanelInfo()
    {
        if (winPanel != null)
        {
            if (winPanel.color.a == 1f)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    GameManager.Instance.AudioManager.PlaySFX("ButtonClick");

                    Time.timeScale = 1f;

                    // Agregar un Upgrade del escenario
                    EnemyModel.OnUpgradeEnemy?.Invoke();

                    GameManager.Instance.PoolerManager.ReturnAllBulletsToPool();

                    Color currentColor = winPanel.color;
                    currentColor.a = 0f;
                    winPanel.color = currentColor;
                }

                if (Input.GetKeyDown(KeyCode.M))
                {
                    GameManager.Instance.AudioManager.PlaySFX("ButtonClick");

                    Time.timeScale = 1f;
                    GameManager.Instance.ScenesManager.ChangeScene("MainMenu", "MainMenuUI");
                    GameManager.Instance.PoolerManager.ReturnAllBulletsToPool();
                }
            }
        }
    }
}
