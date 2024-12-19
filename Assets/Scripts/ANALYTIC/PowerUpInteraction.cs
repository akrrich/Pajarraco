using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Analytics;

public class PowerUpInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    public void SendEvent(string powerUpAction)
    {
        AnalyticsManager.instance.PowerUpPickUp(powerUpAction);
    }
}
