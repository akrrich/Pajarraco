using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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


    public void OnPointerEnter(PointerEventData eventData)
    {
        Cursor.SetCursor(GameManager.Instance.CustomCursorTexture, GameManager.Instance.CursorHotsSpot, CursorMode.Auto);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(GameManager.Instance.DefaultCursorTexture, Vector2.zero, CursorMode.Auto);
    }
}
