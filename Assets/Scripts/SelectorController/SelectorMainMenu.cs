using System;
using System.Collections;
using UnityEngine;

public class SelectorMainMenu : SelectorController
{
    [SerializeField] private GameObject panelSettings;

    private static event Action onPausePanelFind;

    public static Action OnPausePanelFind { get => onPausePanelFind; set => onPausePanelFind = value; }


    protected override void Awake()
    {
        GameManager.Instance.AudioManager.PlayMusic("MainMenu");

        base.Awake();
        StartCoroutine(GetComponents());
    }

    protected override void UpdateSelectorController()
    {
        base.UpdateSelectorController();
        DisablePanelSettings();
    }


    protected override IEnumerator GetComponents()
    {
        yield return new WaitForSeconds(1);

        selectorButton = GameObject.Find("SelectorFather").GetComponent<RectTransform>();
        panelSettings = GameObject.Find("PanelSettings");
        panelSettings.SetActive(false);

        for (int i = 0; i < buttonNames.Length; i++)
        {
            GameObject currentButtonName = GameObject.Find(buttonNames[i]);

            buttons.Add(currentButtonName);
        }
    }

    protected override void Initialize()
    {
        multiplierSelectedElement = 170;
        manualPositionFirstY = -175;
        manualPositionLastY = -715;
    }

    protected override void InteractWithCurrentButton()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GameManager.Instance.AudioManager.PlaySFX("ButtonClick");

            switch (currentIndex)
            {
                case 0:
                    onPausePanelFind?.Invoke();
                    GameManager.Instance.ScenesManager.ChangeScene("Level", "LevelUI");
                    break;

                case 1:
                    panelSettings.SetActive(true);
                    break;

                case 2:
                    GameManager.Instance.ScenesManager.ChangeScene("Credits", "CreditsUI");
                    break;

                case 3:
                    GameManager.Instance.ScenesManager.ExitGame();
                    break;
            }
        }
    }

    private void DisablePanelSettings()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && panelSettings.activeSelf)
        {
            GameManager.Instance.AudioManager.PlaySFX("ButtonClick");
            panelSettings.SetActive(false);
        }
    }
}
