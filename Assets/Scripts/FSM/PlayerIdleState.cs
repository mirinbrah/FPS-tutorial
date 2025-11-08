public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerMovement currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {
        _ctx.SetCurrentSpeed(0f);
        //_ctx.ArmsAnimator.CrossFade("Idle", 0.1f);
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void ExitState() { }

    public override void CheckSwitchStates()
    {
        if (_ctx.IsMovementPressed)
        {
            SwitchState(_factory.Walking());
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
    }
}