using Unity.Mathematics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyBullet enemyBullet; 

    private Player player;

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
        player = FindObjectOfType<Player>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        GameManager.Instance.GameStatePlaying += UpdateEnemy;
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
    }

    void OnTriggerEnter2D(Collider2D collision)
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
}
