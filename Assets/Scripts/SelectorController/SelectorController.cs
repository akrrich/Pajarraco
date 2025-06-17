using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public abstract class SelectorController : MonoBehaviour
{
    [SerializeField] protected GameObject selectorButton;
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
                SetSelectorPositionManual(new Vector3(selectorButton.transform.position.x, 310, selectorButton.transform.position.z));
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
                SetSelectorPositionManual(new Vector3(selectorButton.transform.position.x, -20, selectorButton.transform.position.z));
                return;
            }

            MoveSelectorUp();
        }
    }

    private void MoveSelectorDown()
    {
        GameManager.Instance.AudioManager.PlaySFX("ButtonSelected");
        selectorButton.transform.position += Vector3.down * 110;
    }

    private void MoveSelectorUp()
    {
        GameManager.Instance.AudioManager.PlaySFX("ButtonSelected");
        selectorButton.transform.position += Vector3.up * 110;
    }

    private void SetSelectorPositionManual(Vector3 manualPosition)
    {
        GameManager.Instance.AudioManager.PlaySFX("ButtonSelected");
        selectorButton.transform.position = manualPosition;
    }

    protected abstract IEnumerator GetComponents();

    protected abstract void InteractWithCurrentButton();
}
