using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    private AudioSource clickSound;


    void Start()
    {
        clickSound = GetComponent<AudioSource>();
    }


    public void ButtonStartGame()
    {
        GameManager.Instance.ChangeStateTo(GameState.Playing);
        StartCoroutine(ChangeSceneAfterSound("Level", 0));
    }

    public void ButtonCloseGame()
    {
        StartCoroutine(CloseGameAfterSound());
    }

    public void ButtonCredits()
    {
        GameManager.Instance.ChangeStateTo(GameState.Credits);
        StartCoroutine(ChangeSceneAfterSound("Credits", 2));
    }


    private IEnumerator ChangeSceneAfterSound(string nameScene, int indexButton)
    {
        clickSound.Play();
        buttons[indexButton].interactable = false;
        yield return new WaitForSeconds(clickSound.clip.length);
        SceneManager.LoadScene(nameScene);
    }

    private IEnumerator CloseGameAfterSound()
    {
        clickSound.Play();
        buttons[3].interactable = false;
        yield return new WaitForSeconds(clickSound.clip.length);
        Application.Quit();
    }
}
