using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private BulletPool bulletPool;
    private StateController stateController;
    private int currentHealth;


    private float speed = 5f;

    private float horizontalInput;

    public Rigidbody2D Rb { get => rb; }
    public BulletPool BulletPool { get => bulletPool; }
    public StateController StateController { get =>  stateController; }
    public int maxHealth = 100;
    public HealthBar healthBar;
   


    public float Speed { get => speed; set => speed = value; }


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bulletPool = FindObjectOfType<BulletPool>();

        stateController = new StateController(this);
        stateController.InitializeState(stateController.IdleState);

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        stateController.UpdateState();
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

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;
        healthBar.SetHealth(currentHealth);
    }

    void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
    }

    void FixedUpdate()
    {
        //horizontalInput = Input.GetAxis("Horizontal");
       // rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
    }
}
