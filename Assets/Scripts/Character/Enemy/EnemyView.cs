using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EnemyView
{
    private EnemyModel enemyModel;

    private Slider healthBar;

    private static event Action onEnemyDeath;

    public static Action OnEnemyDeath { get => onEnemyDeath; set => onEnemyDeath = value; }


    public EnemyView(EnemyController enemyController)
    {
        enemyModel = enemyController.EnemyModel;
        SuscribeToEnemyModelHealthBarEvent();
        enemyController.StartCoroutine(SuscribeToEnemyModelUpdageEnemy());
        enemyController.StartCoroutine(FindHealthBar());
    }


    public void UnsuscribeToEnemyModelHealthBarEvent()
    {
        enemyModel.OnUpdateHealthBar -= UpdateHealthBar;
    }

    public void UnsuscribeToEnemyModelUpgradeEnemy()
    {
        EnemyModel.OnUpgradeEnemy -= UpgradeEnemyInformation;
    }


    private void SuscribeToEnemyModelHealthBarEvent()
    {
        enemyModel.OnUpdateHealthBar += UpdateHealthBar;
    }

    // Suscribirlo ultimo para que cuando se ejecute lo actualize con la nueva vida
    private IEnumerator SuscribeToEnemyModelUpdageEnemy()
    {
        yield return null;

        EnemyModel.OnUpgradeEnemy += UpgradeEnemyInformation;
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
        healthBar.maxValue = enemyModel.Life;
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

    private void UpgradeEnemyInformation()
    {
        healthBar.minValue = 0;
        healthBar.maxValue = enemyModel.Life;
        healthBar.value = enemyModel.Life;

        if (healthBar != null)
        {
            healthBar.fillRect.gameObject.SetActive(true);
        }
    }
}
