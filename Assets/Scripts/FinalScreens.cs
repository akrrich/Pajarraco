using UnityEngine;

public class FinalScreens : MonoBehaviour
{
    [SerializeField] private GameObject[] screens;

    private Player player;

    private AudioSource buttonClick;


    void Start()
    {
        player = FindObjectOfType<Player>();
        //buttonClick = GetComponent<AudioSource>();

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
        //buttonClick.Play();

        PlayerEvents.OnMementoLifeChange?.Invoke();

        PlayerEvents.OnPlayerRespawn += player.PlayerMemento.RestoreState;
        PlayerEvents.OnPlayerRespawn?.Invoke();
        PlayerEvents.OnPlayerRespawn -= player.PlayerMemento.RestoreState;

        screens[1].SetActive(false); 

        GameManager.Instance.ChangeStateTo(GameState.Playing);
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
}
