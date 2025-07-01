using System;
using UnityEngine;
using System.Collections;

public class BulletEnemy : Bullet
{
    private PlayerController playerController;
    private EnemyController enemyController;
    private SpriteRenderer spriteRenderer;
    private Transform floor;

    private Coroutine blinkCoroutine;

    private Color baseNormalColor;

    private bool isStayedInFloor = false;


    public BulletEnemy(Transform transform, Action<Bullet> returnToPoolCallback) : base(transform, returnToPoolCallback)
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

        Collisions.DrawRectOnGizmos(floor);
    }


    protected override void GetComponents()
    {
        playerController = UnityEngine.Object.FindFirstObjectByType<PlayerController>();
        enemyController = UnityEngine.Object.FindFirstObjectByType<EnemyController>();
        spriteRenderer = Transform.GetComponentInChildren<SpriteRenderer>();   
        floor = GameObject.Find("Floor").transform;
    }

    protected override void Initialize()
    {
        EnemyModel.OnIncreaseBulletSpeed += IncreaseBulletSpeed;

        baseNormalColor = spriteRenderer.color;
        speed = 15f;
        damage = 1;
    }

    protected override void CheckCollisions()
    {
        if (Transform.gameObject.activeInHierarchy)
        {
            if (Collisions.CollisionWithDownEdge(transform, floor) && !isStayedInFloor)
            {
                isStayedInFloor = true;
                dir = Vector2.zero;

                if (blinkCoroutine != null)
                {
                    enemyController.StopCoroutine(blinkCoroutine);
                }

                blinkCoroutine = enemyController.StartCoroutine(BlinkEffect());
            }

            if (Collisions.CollisionBetweenRects(transform, playerController.transform))
            {
                if (blinkCoroutine != null)
                {
                    enemyController.StopCoroutine(blinkCoroutine);
                    blinkCoroutine = null;
                    spriteRenderer.color = baseNormalColor;
                    isStayedInFloor = false;
                }

                playerController.PlayerModel.GetDamage(damage);
                ReturnToPool();
            }
        }
    }

    public override void ReinitializeSceneReferences()
    {
        playerController = UnityEngine.Object.FindFirstObjectByType<PlayerController>();
        enemyController = UnityEngine.Object.FindFirstObjectByType<EnemyController>();
        floor = GameObject.Find("Floor").transform;

        if (blinkCoroutine != null)
        {
            enemyController.StopCoroutine(blinkCoroutine);
            blinkCoroutine = null;
        }

        spriteRenderer.color = baseNormalColor;
        speed = 15f;
        isStayedInFloor = false;
    }

    public override void ReturnToPool()
    {
        base.ReturnToPool();
        spriteRenderer.color = baseNormalColor;
        isStayedInFloor = false;

        if (blinkCoroutine != null)
        {
            enemyController.StopCoroutine(blinkCoroutine);
            blinkCoroutine = null;
        }
    }

    private IEnumerator BlinkEffect()
    {
        float duration = 3f;
        float elapsed = 0f;

        Color originalColor = baseNormalColor;

        while (elapsed < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

            elapsed += Time.deltaTime;
            yield return null;
        }

        isStayedInFloor = false;
        spriteRenderer.color = baseNormalColor;
        blinkCoroutine = null;
        base.ReturnToPool();
    }

    private void IncreaseBulletSpeed()
    {
        float speedMultiplier = 1f;
        speed += speedMultiplier;
    }
}
