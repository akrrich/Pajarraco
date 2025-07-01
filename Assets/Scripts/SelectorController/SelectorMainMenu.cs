using System;
using System.Collections;
using UnityEngine;

public class SelectorMainMenu : SelectorController
{
    private static event Action onPausePanelFind;

    public static Action OnPausePanelFind { get => onPausePanelFind; set => onPausePanelFind = value; }


    protected override void Awake()
    {
        base.Awake();
        StartCoroutine(GetComponents());
    }

    protected override void UpdateSelectorController()
    {
        base.UpdateSelectorController();
    }


    protected override IEnumerator GetComponents()
    {
        yield return new WaitForSeconds(1);

        selectorButton = GameObject.Find("SelectorFather").GetComponent<RectTransform>();

        for (int i = 0; i < buttonNames.Length; i++)
        {
            GameObject currentButtonName = GameObject.Find(buttonNames[i]);

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
                    onPausePanelFind?.Invoke();
                    GameManager.Instance.ScenesManager.ChangeScene("Level1", "Level1UI");
                    break;

                case 1:
                    // Agregar un panel de opciones
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
}
