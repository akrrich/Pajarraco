using System.Collections;
using UnityEngine;

public class SelectorLevel : SelectorController
{
    protected override void Awake()
    {
        base.Awake();
        StartCoroutine(GetComponents());
    }

    protected override void UpdateSelectorController()
    {
        if (GameManager.Instance.PauseManager.IsGamePaused)
        {
            base.UpdateSelectorController();
        }
    }

    protected override IEnumerator GetComponents()
    {
        yield return new WaitForSeconds(1);

        GameObject canvas = GameObject.Find("CanvasPause");
        GameObject father = canvas.transform.Find("PausePanel").gameObject;

        selectorButton = father.transform.Find("SelectorFather").gameObject;

        for (int i = 0; i < buttonNames.Length; i++)
        {
            GameObject currentButtonName = father.transform.Find(buttonNames[i]).gameObject;

            buttons.Add(currentButtonName);
        }
    }

    protected override void InteractWithCurrentButton()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GameManager.Instance.AudioManager.PlaySFX("ButtonClick");

            switch (currentIndex)
            {
                case 0:
                    GameManager.Instance.PauseManager.ResumeGameButton();
                    break;

                case 1:
                    // Agregar un panel de opciones
                    break;

                case 2:
                    GameManager.Instance.PauseManager.IsGamePaused = false;
                    Time.timeScale = 1f;
                    GameManager.Instance.ScenesManager.ChangeScene("MainMenu", "MainMenuUI");
                    GameManager.Instance.PoolerManager.ReturnAllBulletsToPool();
                    break;

                case 3:
                    GameManager.Instance.ScenesManager.ExitGame();
                    break;
            }
        }
    }
}
