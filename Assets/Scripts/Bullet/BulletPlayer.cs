using System;
using UnityEngine;

public class BulletPlayer : Bullet
{
    private EnemyController enemyController;
    private Transform roof;

    public BulletPlayer(Transform transform, Action<Bullet> returnToPoolCallback) : base(transform, returnToPoolCallback)
    {
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
        enemyController = UnityEngine.Object.FindFirstObjectByType<EnemyController>();
        roof = GameObject.Find("Roof").transform;
    }

    protected override void Initialize()
    {
        speed = 15f;
        damage = 1;
    }

    protected override void CheckCollisions()
    {
        if (Transform.gameObject.activeInHierarchy)
        {
            if (Collisions.CollisionBetweenRects(transform, roof))
            {
                ReturnToPool();
            }

            if (Collisions.CollisionBetweenRects(transform, enemyController.transform))
            {
                enemyController.EnemyModel.GetDamage(damage);
                ReturnToPool();
            }
        }
    }
}
