using System.Collections;
using UnityEngine;

public class FinalScreens : MonoBehaviour
{
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject loosePanel;


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

        winPanel = GameObject.Find("CanvasWinScreen").transform.Find("WinPanel").gameObject;
        loosePanel = GameObject.Find("CanvasLooseScreen").transform.Find("LoosePanel").gameObject;
    }

    private void ShowLoosePanel()
    {
        Time.timeScale = 0f;
        loosePanel.SetActive(true);
    }

    private void ShowWinPanel()
    {
        Time.timeScale = 0f;
        winPanel.SetActive(true);
    }

    private void UpdateLoosePanelInfo()
    {
        if (loosePanel != null)
        {
            if (loosePanel.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    Time.timeScale = 1f;
                    GameManager.Instance.ScenesManager.ChangeScene("Level1", "Level1UI");
                    GameManager.Instance.PoolerManager.ReturnAllBulletsToPool();
                    GameManager.Instance.PoolerManager.ReinitializeBulletsReferences();
                }

                if (Input.GetKeyDown(KeyCode.M))
                {
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
            if (winPanel.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    Time.timeScale = 1f;

                    // Agregar un Upgrade del escenario
                    EnemyModel.OnUpgradeEnemy?.Invoke();

                    GameManager.Instance.PoolerManager.ReturnAllBulletsToPool();
                    winPanel.SetActive(false);
                }

                if (Input.GetKeyDown(KeyCode.M))
                {
                    Time.timeScale = 1f;
                    GameManager.Instance.ScenesManager.ChangeScene("MainMenu", "MainMenuUI");
                    GameManager.Instance.PoolerManager.ReturnAllBulletsToPool();
                }
            }
        }
    }
}
