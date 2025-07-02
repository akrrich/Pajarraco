using UnityEngine;

public class Credits : MonoBehaviour
{
    void Awake()
    {
        GameManager.Instance.AudioManager.PlayMusic("Credits");

        SuscribeToUpdateManagerEvent();
    }

    // Simulacion de Update
    void UpdateCredits()
    {
        ChangeScene();
    }

    void OnDestroy()
    {
        UnsuscribeToUpdateManagerEvent();
    }


    private void SuscribeToUpdateManagerEvent()
    {
        GameManager.Instance.UpdateManager.OnUpdate += UpdateCredits;
    }

    private void UnsuscribeToUpdateManagerEvent()
    {
        GameManager.Instance.UpdateManager.OnUpdate -= UpdateCredits;
    }

    private void ChangeScene()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            GameManager.Instance.ScenesManager.ChangeScene("MainMenu", "MainMenuUI");
        }
    }
}
