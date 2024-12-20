using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Analytics;

public class BulletInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    public void SendEvent(int BulletAction)
    {
        AnalyticsManager.instance.BulletsUsed(BulletAction.ToString());
    }
}
