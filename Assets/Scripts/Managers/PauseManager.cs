using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    private static PauseManager instance;
    public static PauseManager Instance { get { return instance; } }

    private AudioSource actionSound;

    [SerializeField] private GameObject panelPause;
    [SerializeField] private GameObject panelSettings;
    [SerializeField] private Button[] buttons;

    private GameState gameState;


    private bool isGamePaused = false;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        actionSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        PauseStatus();
    }


    public void ResumeGame()
    {
        GameManager.Instance.ChangeStateTo(GameState.Playing);

        actionSound.Play();
        panelPause.SetActive(false);
        panelSettings.SetActive(false);
        isGamePaused = false;
    }

    public void Settings()
    {
        actionSound.Play();
        panelSettings.SetActive(true);
    }

    public void ReturnToMenu()
    {
        StartCoroutine(PlayClickSoundAndChangeScene("Menu", 0));
        isGamePaused = false;

        GameManager.Instance.ChangeStateTo(GameState.Menu);
    }

    public void BackButton()
    {
        actionSound.Play();
        panelSettings.SetActive(false);
    }


    private void PauseStatus()
    {
        if (!isGamePaused)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager.Instance.ChangeStateTo(GameState.Pause);

                actionSound.Play();
                isGamePaused = true;
                panelPause.SetActive(true);
            }
        }
    }


    private IEnumerator PlayClickSoundAndChangeScene(string sceneToLoad, int indexButton)
    {
        actionSound.Play();
        buttons[indexButton].interactable = false;
        yield return new WaitForSeconds(actionSound.clip.length);
        SceneManager.LoadScene(sceneToLoad);
    }
}
