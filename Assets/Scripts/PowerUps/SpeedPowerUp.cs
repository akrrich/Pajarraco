using UnityEngine;

public class SpeedPowerUp : PowerUps
{
    protected override void ActivePowerUp(Collider2D collider)
    {
        Player player = collider.gameObject.GetComponent<Player>();
        SpeedCommand speedCommand = new SpeedCommand(player);
        powerUpsManager.AddPowerUp(speedCommand, powerUpScriptable.DurationTimePowerUp);
    }
}
