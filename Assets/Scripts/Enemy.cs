using Unity.Mathematics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyBullet enemyBullet; 

    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private float leftLimit;
    [SerializeField] private float rightLimit;

    [SerializeField] private int life = 10;
    private int minLife = 1;
    private int currentLife;

    private float speed = 5f;
    private float counterForAttack = 0f;
    private float attackSeconds = 2f;

    private bool movingRight = true;

    public int Life { get => life; set => life = value; }
    public HealthBar healthBar;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentLife = life;
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            TakeDamage(PlayerBullet.Damage);
        }
    }


    private void Move()
    {
        if (movingRight)
        {
            rb.velocity = new Vector2(speed, 0);
        }

        else
        {
            rb.velocity = new Vector2(-speed,0);
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
            Instantiate(enemyBullet, transform.position + new Vector3(0f, -1), quaternion.identity);
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

    private void TakeDamage( int damage)
    {
        currentLife -= damage;
        if (currentLife < 0) currentLife = 0;
        healthBar.SetHealth(currentLife);
    }

    private void Heal(int amount)
    {
        currentLife += amount;
        if (currentLife > life) currentLife = life;
        healthBar.SetHealth(currentLife);
    }
}
