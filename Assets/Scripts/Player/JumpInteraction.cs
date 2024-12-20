using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Analytics;

public class JumpInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    public void SendEvent(int JumpsMade)
    {
        JumpsMade++;
        AnalyticsManager.instance.PlayerDamaged(JumpsMade.ToString());
    }
}
