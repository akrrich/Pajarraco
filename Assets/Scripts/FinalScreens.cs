using UnityEngine;

public class FinalScreens : MonoBehaviour
{
    [SerializeField] private GameObject[] screens;

    private Player player;


    void Start()
    {
        player = FindObjectOfType<Player>();

        PlayerEvents.OnPlayerDefeated += ShowDefeatedScreen;
    }

    void OnDestroy()
    {
        PlayerEvents.OnPlayerDefeated -= ShowDefeatedScreen;
    }


    public void RespawnPlayerButton()
    {
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
}
