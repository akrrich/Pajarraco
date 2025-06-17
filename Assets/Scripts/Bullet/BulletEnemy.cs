using UnityEngine;
using System.Collections;

public class BulletEnemy : Bullet
{
    private PlayerController playerController;
    private Transform floor;

    private Color baseNormalColor;

    private bool isStayedInFloor = false;


    protected override void Awake()
    {
        base.Awake();
        Initialize();
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
        base.GetComponents();

        playerController = FindFirstObjectByType<PlayerController>();
        floor = GameObject.Find("Floor").transform;
    }

    private void Initialize()
    {
        baseNormalColor = spriteRenderer.color;
    }

    protected override void CheckCollisions()
    {
        if (gameObject.activeInHierarchy)
        {
            if (Collisions.CollisionWithDownEdge(transform, floor) && !isStayedInFloor)
            {
                isStayedInFloor = true;
                direction = Vector2.zero;
                StartCoroutine(BlinkEffect());
            }

            if (Collisions.CollisionBetweenRects(transform, playerController.transform))
            {
                playerController.PlayerModel.GetDamage(damage);
                isStayedInFloor = false;
                spriteRenderer.color = baseNormalColor;
                OnReturnBulletToPool();
            }
        }
    }

    private IEnumerator BlinkEffect()
    {
        float duration = 3f;
        float elapsed = 0f;

        Color originalColor = baseNormalColor;

        while (elapsed < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsed / duration); // va de opaco a transparente
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

            elapsed += Time.deltaTime;
            yield return null;
        }

        isStayedInFloor = false;
        spriteRenderer.color = baseNormalColor;
        OnReturnBulletToPool();
    }
}
