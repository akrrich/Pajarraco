using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EnemyView
{
    private EnemyModel enemyModel;

    private Animator animator;
    private SpriteRenderer sr;
    private Slider healthBar;
    private Image fillSliderImage;

    private RuntimeAnimatorController batController;
    private RuntimeAnimatorController eyeController;

    private static event Action onEnemyDeath;

    private bool useFlyEye = false;

    public static Action OnEnemyDeath { get => onEnemyDeath; set => onEnemyDeath = value; }


    public EnemyView(EnemyController enemyController, RuntimeAnimatorController batController, RuntimeAnimatorController eyeController)
    {
        GetComponents(enemyController);
        SuscribeToEnemyModelHealthBarEvent();
        enemyController.StartCoroutine(SuscribeToEnemyModelUpgradeEnemy());
        enemyController.StartCoroutine(FindHealthBar());

        this.eyeController = eyeController;
        this.batController = batController;
        animator.runtimeAnimatorController = eyeController;
    }

    public void UnsuscribeToEnemyModelHealthBarEvent()
    {
        enemyModel.OnUpdateHealthBar -= UpdateHealthBar;
    }

    public void UnsuscribeToEnemyModelUpgradeEnemy()
    {
        EnemyModel.OnUpgradeEnemy -= UpgradeEnemyInformation;
    }

    public void FlipAnim(bool value)
    {
        sr.flipX = value;
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

    private void GetComponents(EnemyController enemyController)
    {
        enemyModel = enemyController.EnemyModel;
        animator = enemyController.GetComponent<Animator>();
        sr = enemyController.GetComponent<SpriteRenderer>();
    }

    private IEnumerator FindHealthBar()
    {
        yield return new WaitForSeconds(1);

        healthBar = GameObject.Find("CanvasEnemyHealthBar").GetComponentInChildren<Slider>();
        fillSliderImage = healthBar.fillRect.GetComponent<Image>();

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
            Color currentColor = fillSliderImage.color;
            currentColor.a = 0f;
            fillSliderImage.color = currentColor;
        }
    }

    private void UpgradeEnemyInformation()
    {
        healthBar.minValue = 0;
        healthBar.maxValue = enemyModel.Life;
        healthBar.value = enemyModel.Life;

        if (healthBar != null)
        {
            Color currentColor = fillSliderImage.color;
            currentColor.a = 1f;
            fillSliderImage.color = currentColor;
        }

        useFlyEye = !useFlyEye;
        animator.runtimeAnimatorController = useFlyEye ? batController : eyeController;

        animator.Rebind();
        animator.Update(0f);
    }
}
