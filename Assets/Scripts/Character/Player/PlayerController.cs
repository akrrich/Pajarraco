using UnityEngine;

public class PlayerController : CharacterController
{
    private PlayerModel playerModel;
    private PlayerView playerView;

    private CustomRigidBody customRB;

    private int leftClick = 0;

    private bool isGrounded = false;

    public PlayerModel PlayerModel { get => playerModel; }


    protected override void Awake()
    {
        base.Awake();
        GetComponents();
    }

    // Simulacion de Update
    protected override void UpdateCharacterController()
    {
        base.UpdateCharacterController();
        PlayerInputs();
        Movement();
        CheckFloorCollision();
    }

    // Simulacion de Gizmos
    protected override void OnDrawGizmosCharacterController()
    { 
        base.OnDrawGizmosCharacterController();

        if (playerModel != null)
        {
            Collisions.DrawRectOnGizmos(playerModel.RightColumn);
            Collisions.DrawRectOnGizmos(playerModel.LeftColumn);
        }
    }

    protected override void GetComponents()
    {
        playerModel = new PlayerModel();
        playerView = new PlayerView();

        customRB = new CustomRigidBody();
    }

    private void Movement()
    {
        Vector2 axis = new Vector2(Input.GetAxisRaw("Horizontal"), 0);

        bool isTouchingRigh = Collisions.CollisionWithRightEdge(transform, playerModel.RightColumn);
        bool isTouchingLeft = Collisions.CollisionWithLeftEdge(transform, playerModel.LeftColumn);

        if (axis.x > 0 && isTouchingRigh)
        {
            axis.x = 0;
        }

        if (axis.x < 0 && isTouchingLeft)
        {
            axis.x = 0;
        }

        customRB.Velocity = new Vector2(axis.x * speed, customRB.Velocity.y);
        customRB.UpdatePhysics();
        customRB.Move(transform);
    }

    private void CheckFloorCollision()
    {
        if (Collisions.CollisionWithDownEdge(transform, playerModel.Floor))
        {
            isGrounded = true;

            float playerHalfHeight = transform.localScale.y / 2f;
            float groundTop = playerModel.Floor.position.y + playerModel.Floor.localScale.y / 2f;

            transform.position = new Vector3(transform.position.x, groundTop + playerHalfHeight, transform.position.z);
            customRB.Velocity = new Vector2(customRB.Velocity.x, 0);
        }

        else
        {
            isGrounded = false;
        }
    }

    private void PlayerInputs()
    {
        if (!GameManager.Instance.PauseManager.IsGamePaused)
        {
            if (Input.GetMouseButtonDown(leftClick))
            {
                playerModel.Attack(transform, Vector2.up);
            }

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                playerModel.Jump(customRB);
            }
        }
    }
}
