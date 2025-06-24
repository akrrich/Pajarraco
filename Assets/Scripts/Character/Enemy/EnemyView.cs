using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyView
{
    private EnemyModel enemyModel;

    private Slider healthBar;


    public EnemyView(EnemyController enemyController)
    {
        enemyModel = enemyController.EnemyModel;
        SuscribeToEnemyModelHealthBarEvent();
        enemyController.StartCoroutine(FindHealthBar());
    }


    public void UnsuscribeToEnemyModelHealthBarEvent()
    {
        enemyModel.OnUpdateHealthBar -= UpdateHealthBar;
    }


    private void SuscribeToEnemyModelHealthBarEvent()
    {
        enemyModel.OnUpdateHealthBar += UpdateHealthBar;
    }

    private IEnumerator FindHealthBar()
    {
        yield return new WaitForSeconds(1);

        healthBar = GameObject.Find("CanvasEnemyHealthBar").GetComponentInChildren<Slider>();

        InitializeHealthBarValues();
    }

    private void InitializeHealthBarValues()
    {
        healthBar.minValue = 0;
        healthBar.maxValue = 5;
        healthBar.value = enemyModel.Life;
    }

    private void UpdateHealthBar()
    {
        healthBar.value = enemyModel.Life;

        if (healthBar.value == 0)
        {
            healthBar.fillRect.gameObject.SetActive(false);
        }
    }
}
