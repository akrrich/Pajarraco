using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider sliderLifeBar;

    private Enemy enemy;

    [SerializeField] private Image fillImage;


    void Start()
    {
        sliderLifeBar = GetComponent<Slider>();
        enemy = FindObjectOfType<Enemy>();

        EnemyEvents.OnEnemyLifeChange += UpdateHealthBar;

        sliderLifeBar.value = enemy.Life;
        sliderLifeBar.maxValue = enemy.MaxLife;
        sliderLifeBar.minValue = enemy.MinLife;
    }

    private void OnDestroy()
    {
        EnemyEvents.OnEnemyLifeChange -= UpdateHealthBar;
    }


    private void UpdateHealthBar()
    {
        int damageReceive = 1;

        sliderLifeBar.value -= damageReceive;

        if (sliderLifeBar.value == enemy.MinLife)
        {
            fillImage.enabled = false;
        }
    }
}
