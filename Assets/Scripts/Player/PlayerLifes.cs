using UnityEngine;

public class PlayerLifes : MonoBehaviour
{
    private Player player;

    [SerializeField] private GameObject[] playerLifes;

    void Start()
    {
        player = FindObjectOfType<Player>();

        PlayerEvents.OnLifeChange += UpdateHearts;
    }

    void OnDestroy()
    {
        PlayerEvents.OnLifeChange -= UpdateHearts;
    }


    private void UpdateHearts()
    {
        int currentLife = player.Life;

        for (int i = 0; i < playerLifes.Length; i++)
        {
            playerLifes[i].SetActive(i < currentLife);
        }
    }
}
