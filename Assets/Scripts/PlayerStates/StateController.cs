public class StateController
{
    private IState currentState;
    public IState CurrentState { get => currentState; }

    private IdleState idleState;
    private MovingState movingState;
    private ShootingState shootingState;

    public IdleState IdleState { get => idleState; }
    public MovingState MovingState { get => movingState; }
    public ShootingState ShootingState { get => shootingState; }


    public StateController(Player player)
    {
        idleState = new IdleState(player);
        movingState = new MovingState(player);
        shootingState = new ShootingState(player);
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
