


public class PowerUpEvent : Unity.Services.Analytics.Event
{

    public PowerUpEvent() : base("powerUpsPicked") { }

    public string actionName { set { SetParameter("actionName", value); } }


}
