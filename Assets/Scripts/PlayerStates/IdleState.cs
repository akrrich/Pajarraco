using UnityEngine;

using UnityEngine.Analytics;
using System.Collections.Generic;


public class IdleState : IState
{
    private Player player;
    int totalShots;

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
        // Transición a estado de movimiento
        if (Mathf.Abs(player.Rb.velocity.x) >= 0.1f)
        {
            player.StateController.TransitionTo(player.StateController.MovingState);
        }

        // Evento de disparo
        if (Input.GetButtonDown("Fire1"))
        {
            totalShots++;
            player.StateController.TransitionTo(player.StateController.ShootingState);

            // Enviar evento a Analytics
            Analytics.CustomEvent("player_shoot", new Dictionary<string, object>
            {
                { "totalShots", totalShots },
                { "timestamp", Time.time }
            });

            Analytics.FlushEvents();

            Debug.Log("Evento 'player_shoot' enviado. Total shots: " + totalShots);
        }

        // Evento de salto
        if (Input.GetKeyDown(KeyCode.Space) && player.IsGrounded)
        {
            player.StateController.TransitionTo(player.StateController.JumpingState);
            Debug.Log("Player saltó.");
        }
    }
}
