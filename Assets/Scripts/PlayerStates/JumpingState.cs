using UnityEngine;

public class JumpingState : IState
{
    private Player player;

    public JumpingState(Player player) 
    { 
        this.player = player;
    }  


    public void Enter()
    {
        player.IsGrounded = false;
        player.PlayerAudios[3].Play();
        player.Rb.AddForce(Vector2.up * player.JumpForce, ForceMode2D.Impulse);
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

        if (Mathf.Abs(player.Rb.velocity.x) == 0f && Mathf.Abs(player.Rb.velocity.y) == 0f)
        {
            player.StateController.TransitionTo(player.StateController.IdleState);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            player.StateController.TransitionTo(player.StateController.ShootingState);
        }
    }
}
