using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject backGround;
    [SerializeField] private Button[] buttons;
    [SerializeField] private GameObject[] panels;
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

    public void ButtonOptions()
    {
        clickSound.Play();
        panels[0].SetActive(true);
    }

    public void ButtonControls()
    {
        clickSound.Play();
        backGround.SetActive(false);
        panels[1].SetActive(true);
    }

    public void ButtonCredits()
    {
        GameManager.Instance.ChangeStateTo(GameState.Credits);
        StartCoroutine(ChangeSceneAfterSound("Credits", 3));
    }

    public void ButtonCloseGame()
    {
        StartCoroutine(CloseGameAfterSound());
    }

    public void ButtonBack()
    {
        clickSound.Play();

        backGround.SetActive(true);

        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(false);
        }
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
        buttons[4].interactable = false;
        yield return new WaitForSeconds(clickSound.clip.length);
        Application.Quit();
    }
}
