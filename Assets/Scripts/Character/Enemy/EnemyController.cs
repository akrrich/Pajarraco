using UnityEngine;

public class EnemyController : CharacterController
{
    private EnemyModel enemyModel;
    private EnemyView enemyView;

    [SerializeField] private RuntimeAnimatorController batController;
    [SerializeField] private RuntimeAnimatorController eyeController;

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

    protected override void OnDestroy()
    {
        base.OnDestroy();
        enemyView.UnsuscribeToEnemyModelHealthBarEvent();
        enemyView.UnsuscribeToEnemyModelUpgradeEnemy();
        enemyModel.UnsuscribeToUpgradeEnemy();
    }

    protected override void GetComponents()
    {
        enemyModel = new EnemyModel();
        enemyView = new EnemyView(this, batController, eyeController);
    }

    private void Movement()
    {
        bool isTouchingRightEdge = Collisions.CollisionWithRightEdge(transform, enemyModel.RightColumn);
        bool isTouchingLeftEdge = Collisions.CollisionWithLeftEdge(transform, enemyModel.LeftColumn);

        if (goRight)
        {
            transform.position += (Vector3.right * enemyModel.Speed * Time.deltaTime);

            if (isTouchingRightEdge)
            {
                goRight = false;
                enemyView.FlipAnim(false);
            }
        }

        else
        {
            transform.position += (Vector3.left * enemyModel.Speed * Time.deltaTime);

            if (isTouchingLeftEdge)
            {
                goRight = true;
                enemyView.FlipAnim(true);
            }
        }
    }
}
