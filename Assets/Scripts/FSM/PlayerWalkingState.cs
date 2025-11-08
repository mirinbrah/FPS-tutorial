using UnityEngine;
public class PlayerWalkingState : PlayerBaseState
{
    public PlayerWalkingState(PlayerMovement currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {
        _ctx.SetCurrentSpeed(_ctx.speed);
        //_ctx.ArmsAnimator.CrossFade("Run", 0.1f);
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void ExitState() { }

    public override void CheckSwitchStates()
    {
        if (!_ctx.IsMovementPressed)
        {
            SwitchState(_factory.Idle());
        }

        else if (!_ctx.IsGrounded)
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
        else if (_ctx.IsMovementPressed && _ctx.IsSprintPressed)
        {
            SwitchState(_factory.Sprinting());
        }
    }
}