public class SpeedCommand : ICommand
{
    private Player player;

    public SpeedCommand(Player player)
    {
        this.player = player;
    }

    
    public void Execute()
    {
        player.ChangeSpeedForPowerUp = true;
        player.Speed = 8f;
    }

    public void Undo()
    {
        player.ChangeSpeedForPowerUp = false;
        player.Speed = 5f;
    }
}
