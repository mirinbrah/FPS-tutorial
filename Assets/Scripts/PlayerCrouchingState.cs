public class PlayerCrouchingState : PlayerBaseState
{
    public PlayerCrouchingState(PlayerMovement currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {
        _ctx.ApplyCrouch();
        _ctx.SetCurrentSpeed(_ctx.crouchSpeed);
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        _ctx.StandUp();
    }

    public override void CheckSwitchStates()
    {
        if (!_ctx.IsGrounded)
        {
            SwitchState(_factory.InAir());
        }
        else if (!_ctx.IsCrouchPressed)
        {
            SwitchState(_factory.Walking());
        }
    }
}