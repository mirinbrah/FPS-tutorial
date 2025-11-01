public class PlayerStateFactory
{
    PlayerMovement _context;

    public PlayerStateFactory(PlayerMovement currentContext)
    {
        _context = currentContext;
    }

    public PlayerBaseState Idle()
    {
        return new PlayerIdleState(_context, this);
    }
    public PlayerBaseState Walking()
    {
        return new PlayerWalkingState(_context, this);
    }
    public PlayerBaseState Sprinting()
    {
        return new PlayerSprintingState(_context, this);
    }
    public PlayerBaseState Crouching()
    {
        return new PlayerCrouchingState(_context, this);
    }
    public PlayerBaseState InAir()
    {
        return new PlayerInAirState(_context, this);
    }
    public PlayerBaseState Grounded() 
    {
        return Idle();
    }
}