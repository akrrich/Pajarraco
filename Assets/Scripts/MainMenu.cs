using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    private AudioSource clickSound;


    void Start()
    {
        clickSound = GetComponent<AudioSource>();
    }


    public void ButtonStartGame()
    {
        GameManager.Instance.ChangeStateTo(GameState.Playing);
        StartCoroutine(ChangeSceneAfterSound("Level"));
    }

    public void ButtonCloseGame()
    {
        StartCoroutine(CloseGameAfterSound());
    }

    public void ButtonCredits()
    {
        GameManager.Instance.ChangeStateTo(GameState.Credits);
        StartCoroutine(ChangeSceneAfterSound("Credits"));
    }


    private IEnumerator ChangeSceneAfterSound(string nameScene)
    {
        clickSound.Play();
        yield return new WaitForSeconds(clickSound.clip.length);
        SceneManager.LoadScene(nameScene);
    }

    private IEnumerator CloseGameAfterSound()
    {
        clickSound.Play();
        yield return new WaitForSeconds(clickSound.clip.length);
        Application.Quit();
    }
}
