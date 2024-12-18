using Unity.Services.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerUpEvent : Unity.Services.Analytics.Event
{

    public PowerUpEvent() : base("powerUpsPicked") { }

    public string actionName { set { SetParameter("actionName", value); } }


}
