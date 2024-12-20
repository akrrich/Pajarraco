public class PlayerEvent : Unity.Services.Analytics.Event
{

    public PlayerEvent() : base("player_damaged") { }

    public string actionName { set { SetParameter("actionName", value); } }

}
