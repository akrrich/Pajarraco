using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private BulletPool bulletPool;
    private StateController stateController;


    private float speed = 5f;

    private float horizontalInput;

    public Rigidbody2D Rb { get => rb; }
    public BulletPool BulletPool { get => bulletPool; }
    public StateController StateController { get =>  stateController; }  

    public float Speed { get => speed; set => speed = value; }


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
        //horizontalInput = Input.GetAxis("Horizontal");
       // rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
    }
}
