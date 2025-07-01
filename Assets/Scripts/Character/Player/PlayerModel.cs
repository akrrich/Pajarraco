using System;
using UnityEngine;

public class PlayerModel
{
    private Transform rightColumn;
    private Transform leftColumn;
    private Transform floor;

    private event Action onUpdateLifesUI; 

    private int life = 3;
    private int minLife = 1;

    private float speed = 10f;
    private float jumpForce = 8f;
    private float timeToShoot = 0f;
    private float maxTimeToShoot = 0.125f;

    public Transform RightColumn { get => rightColumn; }
    public Transform LeftColumn { get => leftColumn; }
    public Transform Floor { get => floor; }

    public Action OnUpdateLifesUI { get => onUpdateLifesUI; set => onUpdateLifesUI = value; }

    public int Life { get => life; }    
    public float Speed { get => speed; }


    public PlayerModel()
    {
        FindObjects();
    }

    public void UpdatePlayerModel()
    {
        UpdateTimeToShootValue();
    }


    public void Attack(Transform firePosition, Vector2 dir)
    {
        if (timeToShoot >= maxTimeToShoot)
        {
            timeToShoot = 0f;

            GameManager.Instance.AudioManager.PlaySFX("PlayerShoot");
            GameManager.Instance.PoolerManager.FireBullet(BulletType.Player, firePosition, dir);
        }
    }

    public void Jump(CustomRigidBody customRB)
    {
        GameManager.Instance.AudioManager.PlaySFX("Jump");
        customRB.Velocity = new Vector2(customRB.Velocity.x, jumpForce);
    }

    public void GetDamage(int damage)
    {
        GameManager.Instance.AudioManager.PlaySFX("PlayerDamage");

        life -= damage;
        onUpdateLifesUI?.Invoke();

        if (life < minLife)
        {
            Death();
        }
    }


    private void FindObjects()
    {
        rightColumn = GameObject.Find("RightWall").transform;
        leftColumn = GameObject.Find("LeftWall").transform;
        floor = GameObject.Find("Floor").transform;
    }

    private void Death()
    {
        PlayerView.OnPlayerDeath?.Invoke();
    }

    private void UpdateTimeToShootValue()
    {
        if (timeToShoot <= maxTimeToShoot)
        {
            timeToShoot += Time.deltaTime;
        }
    }
}
