using UnityEngine;

public class CustomRigidBody
{
    private Vector2 velocity;
    private Vector2 Acceleration;

    private bool UseGravity = true;

    private float gravity = -9.8f;

    public Vector2 Velocity { get => velocity; set => velocity = value; }


    public void UpdatePhysics()
    {
        if (UseGravity)
        {
            Acceleration.y += gravity;
        }

        velocity += Acceleration * Time.deltaTime;
        Acceleration = Vector2.zero;
    }

    public void Move(Transform transform)
    {
        transform.position += (Vector3)(Velocity * Time.deltaTime);
    }

    public void AddForce(Vector2 force)
    {
        Acceleration += force;
    }

    public void SetVelocity(Vector2 newVelocity)
    {
        velocity = newVelocity;
    }

    public void Stop()
    {
        velocity = Vector2.zero;
        Acceleration = Vector2.zero;
    }
}
