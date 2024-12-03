using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallVerticalJumpState : PlayerState
{
    public PlayerWallVerticalJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        rb.velocity = new Vector2(rb.velocity.x, player.jumpForce);
        player.curStamina -= 15f; //体力减少
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(rb.velocity.y < 0)
        {
            if (player.isWallDetected() && player.canClimb)
            {
                stateMachine.changeState(player.wallSlideState);
            }
            else
            {
                stateMachine.changeState(player.fallState);
            }
        }
        if (player.isGroundDetected()) //跳跃到落下
        {
            stateMachine.changeState(player.idleState);
        }
        if (!player.isWallDetected() && player.isBottomWallDetected())
        {
            stateMachine.changeState(player.wallClimbState);
        }
    }
}
