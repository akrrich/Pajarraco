using System;
using UnityEngine;

public class EnemyModel
{
    private Transform rightColumn;
    private Transform leftColumn;

    private event Action onUpdateHealthBar;
    private static event Action onIncreaseBulletSpeed; // Tiene que ser estatico porque se suscribe una unica vez que es cuando se crea el constructor de la bala, por lo tanto si no es estatico despues no existira
    private static event Action onUpgradeEnemy;

    private int life = 10;
    private int minLife = 1;

    private float speed = 10f;
    private float timeToShoot = 0f;
    private float maxTimeToShoot = 3f;

    public Transform RightColumn { get => rightColumn; }
    public Transform LeftColumn { get => leftColumn; }

    public Action OnUpdateHealthBar { get => onUpdateHealthBar; set => onUpdateHealthBar = value; }
    public static Action OnIncreaseBulletSpeed { get => onIncreaseBulletSpeed; set => onIncreaseBulletSpeed = value; }
    public static Action OnUpgradeEnemy { get => onUpgradeEnemy; set => onUpgradeEnemy = value; }

    public int Life { get => life; }  
    public float Speed { get => speed; }    


    public EnemyModel()
    {
        FindObjects();
        SuscribeToUpgradeEnemy();
    }


    public void UnsuscribeToUpgradeEnemy()
    {
        EnemyModel.onUpgradeEnemy -= UpgradeEnemyInformation;
    }

    public void Attack(Transform firePosition, Vector2 dir)
    {
        timeToShoot += Time.deltaTime;

        if (timeToShoot >= maxTimeToShoot)
        {
            timeToShoot = 0f;

            GameManager.Instance.AudioManager.PlaySFX("EnemyShoot");
            GameManager.Instance.PoolerManager.FireBullet(BulletType.Enemy, firePosition, dir);
        }
    }

    public void GetDamage(int damage)
    {
        GameManager.Instance.AudioManager.PlaySFX("EnemyDamage");

        life -= damage;
        onUpdateHealthBar?.Invoke();

        if (life < minLife)
        {
            Death();
            return;
        }

        IncreaseSpeed();
        DecreaseMaxTimeShoot();
        onIncreaseBulletSpeed?.Invoke();
    }


    private void SuscribeToUpgradeEnemy()
    {
        EnemyModel.onUpgradeEnemy += UpgradeEnemyInformation;
    }

    private void FindObjects()
    {
        rightColumn = GameObject.Find("RightWall").transform;
        leftColumn = GameObject.Find("LeftWall").transform;
    }

    private void Death()
    {
        EnemyView.OnEnemyDeath?.Invoke();
    }

    private void IncreaseSpeed()
    {
        float speedMultiplier = 1f;
        speed += speedMultiplier;
    }

    private void DecreaseMaxTimeShoot()
    {
        float maxTimeToShootMultiplier = 0.1f;
        maxTimeToShoot -= maxTimeToShootMultiplier;
    }

    private void UpgradeEnemyInformation()
    {
        life = 20;


    }
}
