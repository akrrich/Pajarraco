using UnityEngine;

public class EnemyController : CharacterController
{
    private EnemyModel enemyModel;
    private EnemyView enemyView;

    private bool goRight = true;

    public EnemyModel EnemyModel { get => enemyModel; }


    protected override void Awake()
    {
        base.Awake();
        GetComponents();
    }

    // Simulacion de Update
    protected override void UpdateCharacterController()
    {
        base.UpdateCharacterController();
        Movement();
        enemyModel.Attack(transform, Vector2.down);
    }

    // Simulacion de Gizmos
    protected override void OnDrawGizmosCharacterController()
    {
        base.OnDrawGizmosCharacterController();

        if (enemyModel != null)
        {
            Collisions.DrawRectOnGizmos(enemyModel.RightColumn);
            Collisions.DrawRectOnGizmos(enemyModel.LeftColumn);
        }
    }

    protected override void GetComponents()
    {
        enemyModel = new EnemyModel();
        enemyView = new EnemyView();
    }

    private void Movement()
    {
        bool isTouchingRightEdge = Collisions.CollisionWithRightEdge(transform, enemyModel.RightColumn);
        bool isTouchingLeftEdge = Collisions.CollisionWithLeftEdge(transform, enemyModel.LeftColumn);

        if (goRight)
        {
            transform.position += (Vector3.right * speed * Time.deltaTime);

            if (isTouchingRightEdge)
            {
                goRight = false;
            }
        }

        else
        {
            transform.position += (Vector3.left * speed * Time.deltaTime);

            if (isTouchingLeftEdge)
            {
                goRight = true;
            }
        }
    }
}
