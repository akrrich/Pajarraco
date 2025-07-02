using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EnemyView
{
    private EnemyModel enemyModel;

    private Animator animator;
    private SpriteRenderer sr;

    private RuntimeAnimatorController batController;
    private RuntimeAnimatorController eyeController;

    private Slider healthBar;

    private bool useFlyEye = false;

    private static event Action onEnemyDeath;

    public static Action OnEnemyDeath { get => onEnemyDeath; set => onEnemyDeath = value; }


    public EnemyView(EnemyController enemyController,
        RuntimeAnimatorController batController,
        RuntimeAnimatorController eyeController)
    {
        enemyModel = enemyController.EnemyModel;
        animator = enemyController.GetComponent<Animator>();
        sr = enemyController.GetComponent<SpriteRenderer>();

        SuscribeToEnemyModelHealthBarEvent();
        enemyController.StartCoroutine(SuscribeToEnemyModelUpgradeEnemy());
        enemyController.StartCoroutine(FindHealthBar());
        this.eyeController = eyeController;
        this.batController = batController;
        animator.runtimeAnimatorController = batController;
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
    private IEnumerator SuscribeToEnemyModelUpgradeEnemy()
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
        useFlyEye = !useFlyEye;
        animator.runtimeAnimatorController = useFlyEye ? eyeController : batController;

        animator.Rebind();
        animator.Update(0f);

    }
    public void FlipAnim(bool value)
    {
        sr.flipX = value;
    }
}
