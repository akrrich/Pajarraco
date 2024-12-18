using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Events;

public class SpeedPowerUp : PowerUps
{

   
    protected override void ActivePowerUp(Collider2D collider)
    {
        Player player = collider.gameObject.GetComponent<Player>();
        SpeedCommand speedCommand = new SpeedCommand(player);
        powerUpsManager.AddPowerUp(speedCommand, powerUpScriptable.DurationTimePowerUp);

        // Enviar evento personalizado a Unity Analytics
        SendPowerUpPickedEvent("Speed");
    }

    private void SendPowerUpPickedEvent(string powerUpType)
    {
        try
        {
            // Enviar un evento con el nombre 'powerUpsPicked' y el tipo de power-up
            Analytics.CustomEvent("powerUpsPicked", new System.Collections.Generic.Dictionary<string, object>
            {
                { "powerUpType", powerUpType },
                { "duration", powerUpScriptable.DurationTimePowerUp }
            });

            Debug.Log($"Evento powerUpsPicked de tipo {powerUpType} enviado a Unity Analytics.");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error al enviar el evento powerUpsPicked: {e.Message}");
        }
    }
}
