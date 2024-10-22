using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private BulletPool bulletPool;
    private StateController stateController;

    private int life = 3;
    private int minLife = 1;

    private float speed = 5f;

    private float horizontalInput;

    public Rigidbody2D Rb { get => rb; }
    public BulletPool BulletPool { get => bulletPool; }
    public StateController StateController { get =>  stateController; }  

    public float Speed { get => speed; set => speed = value; }
    public int Life { get => life; set => life = value; }   


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

        print(Life);
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
