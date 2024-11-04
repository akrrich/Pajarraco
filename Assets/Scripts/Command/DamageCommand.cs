public class DamageCommand : ICommand
{
    private Player player;

    public DamageCommand(Player player)
    {
        this.player = player;
    }

    public void Execute()
    {
        player.TimeToWaitForShoot = 0.25f;
        PlayerBullet.Speed = 8f;
    }

    public void Undo()
    {
        player.TimeToWaitForShoot = 0.5f;
        PlayerBullet.Speed = 6f;
    }
}
