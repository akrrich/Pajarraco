using System;
using UnityEngine;

public abstract class Bullet
{
    protected Vector3 dir;

    protected float speed;
    protected int damage;

    protected Transform transform; // Representa la posicion en la escena

    private Action<Bullet> returnToPoolCallback;

    public Transform Transform { get => transform; set => transform = value; }


    public Bullet(Transform transform, Action<Bullet> returnToPoolCallback)
    {
        this.transform = transform;
        this.returnToPoolCallback = returnToPoolCallback;

        SuscribeToUpdateManagerEvents();
        GetComponents();
        Initialize();
    }


    // Simulacion de Update
    protected virtual void UpdateBullet()
    {
        if (transform != null)
        {
            if (transform.gameObject.activeSelf)
            {
                Movemnt();
                CheckCollisions();
            }
        }
    }

    // Simulacion de Gizmos
    protected virtual void OnDrawGizmosBullet()
    {
        if (transform.gameObject.activeSelf)
        {
            Collisions.DrawRectOnGizmos(transform);
        }
    }


    public void SetDir(Vector3 direction)
    {
        dir = direction.normalized;
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

    private void Movemnt()
    {
        transform.position += dir * speed * Time.deltaTime;
    }

    protected abstract void GetComponents();

    protected abstract void Initialize();
    
    protected abstract void CheckCollisions();

    protected void ReturnToPool()
    {
        returnToPoolCallback?.Invoke(this);
    }
}