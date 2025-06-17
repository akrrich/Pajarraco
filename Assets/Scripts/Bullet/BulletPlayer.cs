using UnityEngine;

public class BulletPlayer : Bullet
{
    private EnemyController enemyController;
    private Transform roof;


    protected override void Awake()
    {
        base.Awake();
    }

    // Simulacon de Update
    protected override void UpdateBullet()
    {
        base.UpdateBullet();
    }

    // Simulacon de Gizmos
    protected override void OnDrawGizmosBullet()
    {
        base.OnDrawGizmosBullet();

        Collisions.DrawRectOnGizmos(roof);
    }


    protected override void GetComponents()
    {
        base.GetComponents();

        enemyController = FindFirstObjectByType<EnemyController>();
        roof = GameObject.Find("Roof").transform;
    }

    protected override void CheckCollisions()
    {
        if (gameObject.activeInHierarchy)
        {
            if (Collisions.CollisionBetweenRects(transform, roof))
            {
                OnReturnBulletToPool();
            }

            if (Collisions.CollisionBetweenRects(transform, enemyController.transform))
            {
                enemyController.EnemyModel.GetDamage(damage);
                OnReturnBulletToPool();
            }
        }
    }
}
