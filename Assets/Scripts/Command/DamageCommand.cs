public class DamageCommand : ICommand
{
    private Player player;

    public DamageCommand(Player player)
    {
        this.player = player;
    }

    public void Execute()
    {
        PlayerBullet.Damage = 2;
    }

    public void Undo()
    {
        PlayerBullet.Damage = 1;
    }
}
