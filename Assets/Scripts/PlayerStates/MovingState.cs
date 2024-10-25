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
            player.Speed = 5f;
        }

        else
        {
            player.Speed = 8f;
        }
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

        if (Input.GetMouseButtonDown(0))
        {
            player.StateController.TransitionTo(player.StateController.ShootingState);
        }
    }
}
