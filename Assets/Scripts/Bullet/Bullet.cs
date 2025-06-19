using System;
using UnityEngine;
public class Bullet
{
    public Vector3 Position { get; protected set; }
    public Vector3 Direction { get; protected set; }
    public float Speed { get; protected set; }

    protected Transform transform;
    protected Transform returnTransform;
    protected Action<Bullet> returnToPoolCallback;

    public Bullet(Transform transform, Action<Bullet> returnToPoolCallback)
    {
        this.transform = transform;
        this.returnToPoolCallback = returnToPoolCallback;
        this.Position = transform.position;

        FindReturnReference("Roof");
    }

    protected virtual void FindReturnReference(string reference)
    {
        returnTransform = GameObject.Find(reference)?.transform;
    }

    public virtual void Init(Vector3 direction, float speed)
    {
        Direction = direction.normalized;
        Speed = speed;
    }

    public virtual void Tick(float deltaTime)
    {
        Position += Direction * Speed * deltaTime;
        transform.position = Position;

        if (Collisions.CollisionBetweenRects(transform, returnTransform))
        {
            ReturnToPool();
        }
    }

    protected virtual void ReturnToPool()
    {
        returnToPoolCallback?.Invoke(this);
    }

    public virtual Transform GetTransform() => transform;
}