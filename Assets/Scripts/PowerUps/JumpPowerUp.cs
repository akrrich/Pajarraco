using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Events;

public class JumpPowerUp : PowerUps
{

    
    protected override void ActivePowerUp(Collider2D collider)
    {
        Player player = collider.gameObject.GetComponent<Player>();
        JumpCommand jumpCommand = new JumpCommand(player);
        powerUpsManager.AddPowerUp(jumpCommand, powerUpScriptable.DurationTimePowerUp);

        // Enviar evento personalizado a Unity Analytics
        SendPowerUpPickedEvent("Jump");
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
