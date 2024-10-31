using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private BulletPool bulletPool;
    private StateController stateController;
    private PlayerMemento playerMemento;

    private int life = 3;
    private int minLife = 1;

    private float speed = 8f;
    private float counterForShoot = 0f;

    private float horizontalInput;

    private bool canShoot = true;
    private bool changeSpeedForPowerUp = false;

    public Rigidbody2D Rb { get => rb; }
    public BulletPool BulletPool { get => bulletPool; }
    public StateController StateController { get =>  stateController; }
    public PlayerMemento PlayerMemento { get => playerMemento; }

    public int Life { get => life; set => life = value; }   
    public float Speed { get => speed; set => speed = value; }
    public float CounterForShoot { set => counterForShoot = value; }    
    public bool ChangeSpeedForPowerUp { get => changeSpeedForPowerUp; set => changeSpeedForPowerUp = value; }
    public bool CanShoot { get => canShoot; set => canShoot = value; }


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        bulletPool = FindObjectOfType<BulletPool>(); 

        stateController = new StateController(this);
        stateController.InitializeState(stateController.IdleState);

        GameManager.Instance.GameStatePlaying += UpdatePlayer;
    }

    void UpdatePlayer()
    {
        stateController.UpdateState();
        CheckIfIsAlive();
        PlayerMovement();
        CheckIfCanShootOrNot();
    }

    void OnDestroy()
    {
        GameManager.Instance.GameStatePlaying -= UpdatePlayer;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            EnemyBullet.ApplyDamge(this);
            PlayerEvents.OnLifeChange?.Invoke();
        }
    }


    private void PlayerMovement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

        if (rb.velocity.x < -0.1)
        {
            spriteRenderer.flipX = true;
        }

        else if (rb.velocity.x > 0.1)
        {
            spriteRenderer.flipX = false;
        }
    }

    private void CheckIfIsAlive()
    {
        if (life < minLife)
        {
            PlayerEvents.OnPlayerDefeated?.Invoke();
            playerMemento = new PlayerMemento(this);
            gameObject.SetActive(false);
        }
    }

    private void CheckIfCanShootOrNot()
    {
        float timeToWait = 0.3f;
        counterForShoot += Time.deltaTime;

        if (counterForShoot >= timeToWait)
        {
            canShoot = true;
        }
    }
}
