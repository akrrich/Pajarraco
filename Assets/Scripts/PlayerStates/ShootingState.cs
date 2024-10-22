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
        Bullet bullet = player.BulletPool.GetBullet();
        bullet.InstantiateBullet(player.transform, player.BulletPool);
    }

    public void Exit()
    {

    }

    public void UpdateState()
    {
        if (Mathf.Abs(player.Rb.velocity.x) == 0f)
        {
            player.StateController.TransitionTo(player.StateController.IdleState);
        }

        if (Mathf.Abs(player.Rb.velocity.x) >= 0.1f)
        {
            player.StateController.TransitionTo(player.StateController.MovingState);
        }
    }
}
