using UnityEngine;

public class JumpPowerUp : PowerUps
{
    protected override void ActivePowerUp(Collider2D collider)
    {
        Player player = collider.gameObject.GetComponent<Player>();
        JumpCommand jumpCommand = new JumpCommand(player);
        powerUpsManager.AddPowerUp(jumpCommand, powerUpScriptable.DurationTimePowerUp);
    }
}
