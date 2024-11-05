using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FinalScreens : MonoBehaviour
{
    [SerializeField] private GameObject[] screens;

    private Player player;
    private Enemy enemy;

    private AudioSource clickSound;


    void Start()
    {
        player = FindObjectOfType<Player>();
        enemy = FindObjectOfType<Enemy>();
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
        Time.timeScale = 0f;

        clickSound.Play();   

        PlayerEvents.OnMementoLifeChange?.Invoke();

        PlayerEvents.OnPlayerRespawn += player.PlayerMemento.RestoreState;
        PlayerEvents.OnPlayerRespawn?.Invoke();
        PlayerEvents.OnPlayerRespawn -= player.PlayerMemento.RestoreState;

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
        GameManager.Instance.ChangeStateTo(GameState.Defeated);
        screens[1].SetActive(true);
    } 

    private void ShowTotalDeathScreen()
    {
        GameManager.Instance.ChangeStateTo(GameState.TotalDefeated);
        screens[2].SetActive(true);
    }

    private IEnumerator ChangeSceneAfterSound(string nameScene)
    {
        clickSound.Play();
        yield return new WaitForSeconds(clickSound.clip.length);
        SceneManager.LoadScene(nameScene);
    }
}
