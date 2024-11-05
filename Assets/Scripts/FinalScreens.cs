using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.EventSystems;

public class FinalScreens : MonoBehaviour
{
    [SerializeField] private GameObject[] screens;

    private Player player;

    private AudioSource clickSound;


    void Start()
    {
        player = FindObjectOfType<Player>();
        clickSound = GetComponent<AudioSource>();

        PlayerEvents.OnPlayerDefeated += ShowDefeatedScreen;
        PlayerEvents.OnPlayerTotalDeath += ShowTotalDeathScreen;

        EnemyEvents.OnEnemyDeath += ShowWinScreen;
    }

    void OnDestroy()
    {
        PlayerEvents.OnPlayerDefeated -= ShowDefeatedScreen;
        PlayerEvents.OnPlayerTotalDeath -= ShowTotalDeathScreen;

        EnemyEvents.OnEnemyDeath -= ShowWinScreen;
    }


    public void RespawnPlayerButton()
    {
       
        clickSound.Play();
        

        PlayerEvents.OnMementoLifeChange?.Invoke();

        PlayerEvents.OnPlayerRespawn += player.PlayerMemento.RestoreState;
        PlayerEvents.OnPlayerRespawn?.Invoke();
        PlayerEvents.OnPlayerRespawn -= player.PlayerMemento.RestoreState;
        player.PlayerAudios[2].Play();

        screens[1].SetActive(false); 

        GameManager.Instance.ChangeStateTo(GameState.Playing);
    }

    public void PlayAgainLevelButton()
    {
        PlayerEvents.OnPlayerDefeated -= ShowDefeatedScreen;
        PlayerEvents.OnPlayerTotalDeath -= ShowTotalDeathScreen;

        EnemyEvents.OnEnemyDeath -= ShowWinScreen;

        StartCoroutine(ChangeSceneAfterSound("Level"));
        GameManager.Instance.ChangeStateTo(GameState.Playing);
    }

    public void ReturnToMenuButton()
    {
        StartCoroutine(ChangeSceneAfterSound("Menu"));
        GameManager.Instance.ChangeStateTo(GameState.Menu);
    }

    public void CreditsButton()
    {
        StartCoroutine(ChangeSceneAfterSound("Credits"));
        GameManager.Instance.ChangeStateTo(GameState.Credits);
    }


    private void ShowWinScreen()
    {
        GameManager.Instance.ChangeStateTo(GameState.Win);
        screens[0].SetActive(true);
    }

    private void ShowDefeatedScreen()
    {
        GameManager.Instance.ChangeStateTo(GameState.Lose);
        screens[1].SetActive(true);
    } 

    private void ShowTotalDeathScreen()
    {
        GameManager.Instance.ChangeStateTo(GameState.Lose);
        screens[2].SetActive(true);
    }

    private IEnumerator ChangeSceneAfterSound(string nameScene)
    {
        clickSound.Play();
        yield return new WaitForSeconds(clickSound.clip.length);
        SceneManager.LoadScene(nameScene);
    }
}
