public class DamageCommand : ICommand
{
    private Player player;

    public DamageCommand(Player player)
    {
        this.player = player;
    }

    public void Execute()
    {
        player.TimeToWaitForShoot = 0.15f;
    }

    public void Undo()
    {
        player.TimeToWaitForShoot = 0.3f;
    }
}
