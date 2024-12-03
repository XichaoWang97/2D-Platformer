using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallClimbState : PlayerState //爬墙到顶自动爬上去
{
    public PlayerWallClimbState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (player.isBottomWallDetected())
        {
            rb.velocityY = player.climbSpeed;
        }
        if (!player.isBottomWallDetected())
        {
            player.setVelocity(player.moveSpeed * player.facingDir, 0);
        }
        if (player.v0Down() && player.v1Down())
        {
            stateMachine.changeState(player.idleState);
        }
    }
}
