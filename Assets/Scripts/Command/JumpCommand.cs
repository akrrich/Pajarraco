public class JumpCommand : ICommand
{
    private Player player;

    public JumpCommand(Player player)
    {
        this.player = player; 
    }


    public void Execute()
    {
        player.JumpForce = 8f;
    }

    public void Undo()
    {
        player.JumpForce = 6f;
    }
}
