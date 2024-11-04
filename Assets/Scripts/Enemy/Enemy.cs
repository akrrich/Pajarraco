using Unity.Mathematics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyBullet enemyBullet; 

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;
    private AudioSource[] enemySounds; // indice 0 = colision, indice 1 = muerte

    [SerializeField] private float leftLimit;
    [SerializeField] private float rightLimit;

    private int life = 20;
    private int minLife = 0;
    private int maxLife = 20;

    private float speed = 7.5f;
    private float counterForAttack = 0f;
    private float attackSeconds = 2f;

    private bool movingRight = true;

    public int Life { get => life; set => life = value; }
    public int MinLife { get => minLife; set => minLife = value; }
    public int MaxLife { get => maxLife; set => maxLife = value; }


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        enemySounds = GetComponents<AudioSource>();

        GameManager.Instance.GameStatePlaying += UpdateEnemy;
        GameManager.Instance.GameStateLose += StopPhysics;
        GameManager.Instance.GameStateWin += StopPhysics;
    }

    void UpdateEnemy()
    {
        CheckLimits();
        Attack();
        CheckIfIsAlive();
        Move();
    }

    void OnDestroy()
    {
        GameManager.Instance.GameStatePlaying -= UpdateEnemy;
        GameManager.Instance.GameStateLose -= StopPhysics;
        GameManager.Instance.GameStateWin -= StopPhysics;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            PlayerBullet.ApplyDamge(this);
            EnemyEvents.OnEnemyLifeChange?.Invoke();

            ManageSounds();
            IncreaseDifficulty();
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

        if (life < maxLife / 2)
        {
            attackSeconds = 1.25f;
        }

        if (counterForAttack >= attackSeconds)
        {
            Instantiate(enemyBullet, transform.position + new Vector3(0f, -1), quaternion.identity);
            counterForAttack = 0f;
        }
    }

    private void CheckIfIsAlive()
    {
        if (life <= minLife)
        {
            EnemyEvents.OnEnemyDeath?.Invoke();

            rb.isKinematic = true;
            spriteRenderer.enabled = false;
            boxCollider2D.enabled = false;

            Destroy(gameObject, enemySounds[1].clip.length);
        }
    }

    private void StopPhysics()
    {
        rb.velocity = Vector2.zero;
    }

    private void ManageSounds()
    {
        if (life > minLife)
        {
            enemySounds[0].Play();
        }

        else
        {
            enemySounds[1].Play();
        }
    }

    private void IncreaseDifficulty()
    {
        float speedEnhancer = 0.3f;
        speed += speedEnhancer;

        enemyBullet.IncreaseSpeed();
    }
}
