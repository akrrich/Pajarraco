using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private BulletPool bulletPool;
    private StateController stateController;

    private int life = 3;
    private int minLife = 1;

    public float speed = 8f;

    private float horizontalInput;

    private bool changeSpeedForPowerUp = false;

    public Rigidbody2D Rb { get => rb; }
    public BulletPool BulletPool { get => bulletPool; }
    public StateController StateController { get =>  stateController; }  

    public int Life { get => life; set => life = value; }   
    public float Speed { get => speed; set => speed = value; }
    public bool ChangeSpeedForPowerUp { get => changeSpeedForPowerUp; set => changeSpeedForPowerUp = value; }


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bulletPool = FindObjectOfType<BulletPool>(); 

        stateController = new StateController(this);
        stateController.InitializeState(stateController.IdleState);
    }

    void Update()
    {
        stateController.UpdateState();
        CheckIfIsAlive();
        float moveInput = Input.GetAxis("Horizontal");


        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);


        anim.SetFloat("Speed", Mathf.Abs(moveInput));


        if (moveInput < 0)
        {

            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (moveInput > 0)
        {

            GetComponent<SpriteRenderer>().flipX = false;
        }

    }

    void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            EnemyBullet.ApplyDamge(this);
        }
    }


    private void CheckIfIsAlive()
    {
        if (life < minLife)
        {
            // se aplicaria memento aca
        }
    }
}
