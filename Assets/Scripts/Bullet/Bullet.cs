using System;
using Unity.Properties;
using UnityEngine;

public class Bullet
{
    public Vector3 Position { get; set; }
    private Vector3 dir;

    private float speed = 15;

    private Transform transform;
    private Transform roof;
    private Action<Bullet> returnToPoolCallback;


    public Bullet(Transform transform, Action<Bullet> returnToPoolCallback)
    {
        this.transform = transform;
        this.returnToPoolCallback = returnToPoolCallback;
        this.Position = transform.position;

        roof = GameObject.Find("Roof").transform;

        SuscribeToUpdateManagerEvents();
    }


    // Simulacion de Update
    void UpdateBullet()
    {
        if (transform.gameObject.activeSelf)
        {
            Tick();
        }
    }

    // Simulacion de Gizmos
    void OnDrawGizmosBullet()
    {
        Collisions.DrawRectOnGizmos(transform);
    }


    private void SuscribeToUpdateManagerEvents()
    {
        GameManager.Instance.UpdateManager.OnUpdate += UpdateBullet;
        GameManager.Instance.UpdateManager.OnDrawGizmos += OnDrawGizmosBullet;
    }

    // Para un futuro
    private void UnsuscribeToUpdateManagerEvents()
    {
        GameManager.Instance.UpdateManager.OnUpdate -= UpdateBullet;
        GameManager.Instance.UpdateManager.OnDrawGizmos -= OnDrawGizmosBullet;
    }

    public void Init(Vector3 direction)
    {
        dir = direction.normalized;
    }

    private void Tick()
    {
        Position += dir * speed * Time.deltaTime;
        transform.position = Position;

        if (Collisions.CollisionBetweenRects(transform, roof))
        {
            ReturnToPool();
        }
    }

    private void ReturnToPool()
    {
        returnToPoolCallback?.Invoke(this);
    }

    public Transform GetTransform() => transform;
}