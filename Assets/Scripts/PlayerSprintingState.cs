public class PlayerSprintingState : PlayerBaseState
{
    public PlayerSprintingState(PlayerMovement currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {
        _ctx.SetCurrentSpeed(_ctx.sprintSpeed);
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void ExitState() { }

    public override void CheckSwitchStates()
    {
        if (!_ctx.IsGrounded)
        {
            SwitchState(_factory.InAir());
        }
        else if (_ctx.IsJumpPressed)
        {
            _ctx.PerformJump();
            SwitchState(_factory.InAir());
        }
        else if (_ctx.IsCrouchPressed)
        {
            SwitchState(_factory.Crouching());
        }
        else if (!_ctx.IsMovementPressed || !_ctx.IsSprintPressed)
        {
            SwitchState(_factory.Walking());
        }
    }
}