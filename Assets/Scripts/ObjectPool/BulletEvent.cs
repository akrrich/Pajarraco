public class BulletEvent : Unity.Services.Analytics.Event
{

    public BulletEvent() : base("player_shoot") { }

    public string actionName { set { SetParameter("actionName", value); } }


}
