public class StateController
{
    private IState currentState;

    private IdleState idleState;
    private MovingState movingState;
    private ShootingState shootingState;
    private JumpingState jumpingState;

    public IdleState IdleState { get => idleState; }
    public MovingState MovingState { get => movingState; }
    public ShootingState ShootingState { get => shootingState; }
    public JumpingState JumpingState { get => jumpingState; }


    public StateController(Player player)
    {
        idleState = new IdleState(player);
        movingState = new MovingState(player);
        shootingState = new ShootingState(player);
        jumpingState = new JumpingState(player);
    }

    public void InitializeState(IState state)
    {
        currentState = state;
        state.Enter();
    }

    public void TransitionTo(IState state)
    {
        currentState.Exit();
        currentState = state;
        currentState.Enter();
    }

    public void UpdateState()
    {
        currentState.UpdateState();
    }
}
