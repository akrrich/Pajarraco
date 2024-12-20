public class JumpEvent : Unity.Services.Analytics.Event
{

    public JumpEvent() : base("player_jump") { }

    public string actionName { set { SetParameter("actionName", value); } }


}
