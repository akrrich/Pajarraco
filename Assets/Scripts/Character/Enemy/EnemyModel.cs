using System;
using UnityEngine;

public class EnemyModel
{
    /// <summary>
    /// Disparar un evento cuando recibe daño para que lo reciban las balas y puedan aumentar su velocidad y duracion en el suelo
    /// </summary>
    /// 
    private Transform rightColumn;
    private Transform leftColumn;

    private event Action onUpdateHealthBar;

    private int life = 5;
    private int minLife = 1;

    private float speed = 10f;
    private float timeToShoot = 0f;
    private float maxTimeToShoot = 5f;

    public Transform RightColumn { get => rightColumn; }
    public Transform LeftColumn { get => leftColumn; }

    public Action OnUpdateHealthBar { get => onUpdateHealthBar; set => onUpdateHealthBar = value; }

    public int Life { get => life; }  
    public float Speed { get => speed; }    


    public EnemyModel()
    {
        FindObjects();
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
        IncreaseSpeed();

        if (life < minLife)
        {
            Death();
        }
    }


    private void FindObjects()
    {
        rightColumn = GameObject.Find("RightWall").transform;
        leftColumn = GameObject.Find("LeftWall").transform;
    }

    private void Death()
    {
        Debug.Log("MurioEnemigo");
    }

    private void IncreaseSpeed()
    {
        float speedMultiplier = 1f;

        speed += speedMultiplier;
    }
}
