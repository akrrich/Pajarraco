using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Analytics;

public class PlayerInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    public void SendEvent(int DamageTaken)
    {
        DamageTaken++;
        AnalyticsManager.instance.PlayerDamaged(DamageTaken.ToString());
    }
}
