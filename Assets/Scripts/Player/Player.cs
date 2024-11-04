using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D boxCollider2D;
    private AudioSource[] playerAudios; // indice 0 = colision, indice 1 = muerte, indice 2 = respawn, que ya esta
    private SpriteRenderer spriteRenderer;
    private BulletPool bulletPool;
    private StateController stateController;
    private PlayerMemento playerMemento;

    private int mementoLife = 3;
    private int mementoMinLife = 0;

    private int life = 3;
    private int minLife = 0;
    private int maxLife = 3;

    private float speed = 8f;
    private float counterForShoot = 0f;
    private float timeToWaitForShoot = 0.5f;

    private float horizontalInput;

    private bool canShoot = true;
    private bool changeSpeedForPowerUp = false;

    public Rigidbody2D Rb { get => rb; }
    public AudioSource[] PlayerAudios { get => playerAudios; set => playerAudios = value; }
    public SpriteRenderer SpriteRenderer { get => spriteRenderer; set => spriteRenderer = value; }
    public BulletPool BulletPool { get => bulletPool; }
    public StateController StateController { get =>  stateController; }
    public PlayerMemento PlayerMemento { get => playerMemento; }

    public int MementoLife { get => mementoLife; set => mementoLife = value; }
    public int Life { get => life; set => life = value; }   
    public int MinLife { get => minLife; set => minLife = value; }  
    public int MaxLife { get => maxLife; set => maxLife = value; }
    public float Speed { get => speed; set => speed = value; }
    public float CounterForShoot { set => counterForShoot = value; }   
    public float TimeToWaitForShoot { get => timeToWaitForShoot; set => timeToWaitForShoot = value; }
    public bool ChangeSpeedForPowerUp { get => changeSpeedForPowerUp; set => changeSpeedForPowerUp = value; }
    public bool CanShoot { get => canShoot; set => canShoot = value; }


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        playerAudios = GetComponents<AudioSource>();
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
            ManageSounds();
        }
    }


    public void EnabledOrDisablePlayer(RigidbodyType2D rbType, bool spriteRenderer, bool boxCollider2D)
    {
        rb.bodyType = rbType;
        this.spriteRenderer.enabled = spriteRenderer;
        this.boxCollider2D.enabled = boxCollider2D;
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
        if (life <= minLife)
        {
            mementoLife--;

            if (mementoLife == mementoMinLife - 1)
            {
                PlayerEvents.OnPlayerTotalDeath?.Invoke();
            }

            else
            {
                PlayerEvents.OnPlayerDefeated?.Invoke();
                playerMemento = new PlayerMemento(this, this);
            }

            EnabledOrDisablePlayer(RigidbodyType2D.Static, false, false);
        }
    }

    private void CheckIfCanShootOrNot()
    {
        counterForShoot += Time.deltaTime;

        if (counterForShoot >= timeToWaitForShoot)
        {
            canShoot = true;
        }
    }

    private void ManageSounds()
    {
        if (life > minLife)
        {
            playerAudios[0].Play();
        }

        else
        {
            playerAudios[1].Play();
        }
    }
}
