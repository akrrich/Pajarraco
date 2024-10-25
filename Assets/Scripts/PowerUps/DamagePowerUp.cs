using UnityEngine;

public class DamagePowerUp : PowerUps
{
    protected override void ActivePowerUp(Collider2D collider)
    {
        Player player = collider.gameObject.GetComponent<Player>();
        DamageCommand damageCommand = new DamageCommand(player);
        powerUpsManager.AddPowerUp(damageCommand, powerUpScriptable.DurationTimePowerUp);
    }
}
