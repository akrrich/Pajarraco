using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public abstract class SelectorController : MonoBehaviour
{
    [SerializeField] protected RectTransform selectorButton;
    [SerializeField] protected List<GameObject> buttons =  new List<GameObject>();

    [SerializeField] protected string[] buttonNames;

    protected int currentIndex = 0;


    protected virtual void Awake()
    {
        SuscribeToUpdateManagerEvent();
    }

    // Simulacion de Update
    protected virtual void UpdateSelectorController()
    {
        ChangeButtonIndex();
        InteractWithCurrentButton();   
    }

    void OnDestroy()
    {
        UnsuscribeToUpdateManagerEvent();
    }


    private void SuscribeToUpdateManagerEvent()
    {
        GameManager.Instance.UpdateManager.OnUpdate += UpdateSelectorController;
    }

    private void UnsuscribeToUpdateManagerEvent()
    {
        GameManager.Instance.UpdateManager.OnUpdate -= UpdateSelectorController;
    }

    private void ChangeButtonIndex()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            currentIndex++;
            if (currentIndex >= buttons.Count)
            {
                currentIndex = 0;
                SetSelectorPositionManual(new Vector3(selectorButton.anchoredPosition.x, 0, 0));
                return;
            }

            MoveSelectorDown();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            currentIndex--;
            if (currentIndex < 0)
            {
                currentIndex = buttons.Count - 1;
                SetSelectorPositionManual(new Vector3(selectorButton.anchoredPosition.x, -585, 0));
                return;
            }

            MoveSelectorUp();
        }
    }

    private void MoveSelectorDown()
    {
        GameManager.Instance.AudioManager.PlaySFX("ButtonSelected");
        selectorButton.anchoredPosition += Vector2.down * 195;
    }

    private void MoveSelectorUp()
    {
        GameManager.Instance.AudioManager.PlaySFX("ButtonSelected");
        selectorButton.anchoredPosition += Vector2.up * 195;
    }

    private void SetSelectorPositionManual(Vector3 manualPosition)
    {
        GameManager.Instance.AudioManager.PlaySFX("ButtonSelected");
        selectorButton.anchoredPosition = manualPosition;
    }

    protected abstract IEnumerator GetComponents();

    protected abstract void InteractWithCurrentButton();
}
