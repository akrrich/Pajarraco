using UnityEngine;

public class IdleState : IState
{
    private Player player;

    public IdleState(Player player)
    {
        this.player = player;
    }


    public void Enter()
    {

    }

    public void Exit()
    {

    }

    public void UpdateState()
    {
        if (Mathf.Abs(player.Rb.velocity.x) >= 0.1f)
        {
            player.StateController.TransitionTo(player.StateController.MovingState);
        }

        if (Input.GetMouseButtonDown(0))
        {
            player.StateController.TransitionTo(player.StateController.ShootingState);
        }
    }
}
