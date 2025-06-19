using UnityEngine;

public class EnemyModel
{
    private ObjectPooler bulletPlayerPool;

    private Transform rightColumn;
    private Transform leftColumn;

    private int life = 3;
    private int minLife = 1;

    private float timeToShoot = 0f;
    private float maxTimeToShoot = 5f;

    public Transform RightColumn { get => rightColumn; }
    public Transform LeftColumn { get => leftColumn; }


    public EnemyModel()
    {
        FindObjects();
    }


    private void FindObjects()
    {
        rightColumn = GameObject.Find("RightWall").transform;
        leftColumn = GameObject.Find("LeftWall").transform;

       // bulletPlayerPool = GameObject.Find("BulletEnemyPool").GetComponent<ObjectPooler>();
    }

    private void Death()
    {
        Debug.Log("MurioEnemigo");
    }


    public void Attack(Transform firePosition, Vector2 dir)
    {
        timeToShoot += Time.deltaTime;

        if (timeToShoot >= maxTimeToShoot)
        {
            timeToShoot = 0f;

            GameManager.Instance.AudioManager.PlaySFX("EnemyShoot");

            GameManager.Instance.FireBullet(firePosition.position, dir,true, 15);
        }
    }

    public void GetDamage(int damage)
    {
        GameManager.Instance.AudioManager.PlaySFX("EnemyDamage");

        life -= damage;

        if (life < minLife)
        {
            Death();
        }
    }
}
