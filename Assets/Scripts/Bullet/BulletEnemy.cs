using System;
using UnityEngine;
using System.Collections;

public class BulletEnemy : Bullet
{
    private PlayerController playerController;
    private SpriteRenderer spriteRenderer;
    private Transform floor;

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
        spriteRenderer = Transform.GetComponentInChildren<SpriteRenderer>();   
        floor = GameObject.Find("Floor")?.transform;
    }

    protected override void Initialize()
    {
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
                playerController.StartCoroutine(BlinkEffect());
            }

            if (Collisions.CollisionBetweenRects(transform, playerController.transform))
            {
                playerController.PlayerModel.GetDamage(damage);
                isStayedInFloor = false;
                spriteRenderer.color = baseNormalColor;
                ReturnToPool();
            }
        }
    }

    public override void ReinitializeSceneReferences()
    {
        playerController = UnityEngine.Object.FindFirstObjectByType<PlayerController>();
        floor = GameObject.Find("Floor")?.transform;
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
        ReturnToPool();
    }
}
