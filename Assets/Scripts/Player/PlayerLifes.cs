using UnityEngine;

public class PlayerLifes : MonoBehaviour
{
    private Player player;

    [SerializeField] private GameObject[] playerLifes;

    void Start()
    {
        player = FindObjectOfType<Player>();

        PlayerEvents.OnMementoLifeChange += UpdateHearts;
    }

    void OnDestroy()
    {
        PlayerEvents.OnMementoLifeChange -= UpdateHearts;
    }


    private void UpdateHearts()
    {
        int currentLife = player.MementoLife;

        for (int i = 0; i < playerLifes.Length; i++)
        {
            playerLifes[i].SetActive(i < currentLife);
        }
    }
}
