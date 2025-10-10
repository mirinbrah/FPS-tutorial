public abstract class PlayerBaseState
{
    protected PlayerMovement _ctx;
    protected PlayerStateFactory _factory;

    public PlayerBaseState(PlayerMovement currentContext, PlayerStateFactory playerStateFactory)
    {
        _ctx = currentContext;
        _factory = playerStateFactory;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
    public abstract void CheckSwitchStates();

    protected void SwitchState(PlayerBaseState newState)
    {
        ExitState();

        newState.EnterState();

        _ctx.CurrentState = newState;
    }
}