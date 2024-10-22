using Unity.Mathematics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyBullet enemyBullet; 

    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private float leftLimit;
    [SerializeField] private float rightLimit;

    private int life = 10;
    private int minLife = 1;

    private float speed = 5f;
    private float counterForAttack = 0f;
    private float attackSeconds = 2f;

    private bool movingRight = true;

    public int Life { get => life; set => life = value; }


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        CheckLimits();
        Attack();
        CheckIfIsAlive();
    }

    void FixedUpdate()
    {
        Move();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            PlayerBullet.ApplyDamge(this);
        }
    }


    private void Move()
    {
        if (movingRight)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }

        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
    }

    private void CheckLimits()
    {
        if (transform.position.x >= rightLimit)
        {
            movingRight = false;
        }

        else if (transform.position.x <= leftLimit)
        {
            movingRight = true; 
        }
    }

    private void Attack()
    {
        counterForAttack += Time.deltaTime;

        if (counterForAttack >= attackSeconds)
        {
            Vector3 bulletSpawnPosition = transform.position - (Vector3)enemyBullet.OffsetBulletPosition;

            Instantiate(enemyBullet, bulletSpawnPosition, quaternion.identity);
            counterForAttack = 0f;
        }
    }

    private void CheckIfIsAlive()
    {
        if (life < minLife)
        {
            // condicion de derrota
        }
    }
}
