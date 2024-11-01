using UnityEngine.UI;
using UnityEngine;

public class PlayerHealthBar : MonoBehaviour
{
    private Slider sliderLifeBar;

    private Player player;

    [SerializeField] private Image fillImage;


    void Start()
    {
        sliderLifeBar = GetComponent<Slider>();
        player = FindObjectOfType<Player>();

        PlayerEvents.OnLifeChange += UpdateHealthBar;

        sliderLifeBar.value = player.Life;
        sliderLifeBar.maxValue = player.MaxLife;
        sliderLifeBar.minValue = player.MinLife;
    }

    private void OnDestroy()
    {
        PlayerEvents.OnLifeChange -= UpdateHealthBar;
    }


    private void UpdateHealthBar()
    {
        sliderLifeBar.value = player.Life;

        if (sliderLifeBar.value == player.MinLife)
        {
            fillImage.enabled = false;
        }

        else
        {
            fillImage.enabled = true;
        }
    }
}
