using UnityEngine;

public class MovingState : IState
{
    private Player player;

    public MovingState(Player player)
    {
        this.player = player;
    }


    public void Enter()
    {
        if (!player.ChangeSpeedForPowerUp)
        {
            player.Speed = 8f;
        }

        else
        {
            player.Speed = 10.5f;
        }
    }

    public void Exit()
    {

    }

    public void UpdateState()
    {
        if (Mathf.Abs(player.Rb.velocity.x) == 0f && Mathf.Abs(player.Rb.velocity.y) == 0f)
        {
            player.StateController.TransitionTo(player.StateController.IdleState);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            player.StateController.TransitionTo(player.StateController.ShootingState);
        }

        if (Input.GetKeyDown(KeyCode.Space) && player.IsGrounded)
        {
            player.StateController.TransitionTo(player.StateController.JumpingState);
        }
    }
}
