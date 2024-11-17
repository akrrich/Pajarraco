using UnityEngine;

public class ShootingState : IState
{
    private Player player;

    public ShootingState(Player player)
    {
        this.player = player;
    }


    public void Enter()
    {
        if (player.CanShoot)
        {
            player.BulletPool.GetBullet().InstantiateBullet(player.transform, player.BulletPool);
            player.CounterForShoot = 0f;
        }
    }

    public void Exit()
    {
        player.CanShoot = false;
    }

    public void UpdateState()
    {
        // chequear este error
        /*if (!player.IsGrounded && Input.GetButtonDown("Fire1"))
        {
            Enter();
            Exit();
        }*/

        if (Mathf.Abs(player.Rb.velocity.x) == 0f && Mathf.Abs(player.Rb.velocity.y) == 0f)
        {
            player.StateController.TransitionTo(player.StateController.IdleState);
        }

        if (Mathf.Abs(player.Rb.velocity.x) >= 0.1f)
        {
            player.StateController.TransitionTo(player.StateController.MovingState);
        }

        if (Input.GetKeyDown(KeyCode.Space) && player.IsGrounded)
        {
            player.StateController.TransitionTo(player.StateController.JumpingState);
        }
    }
}
