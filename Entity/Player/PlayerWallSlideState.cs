using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using UnityEngine.Playables;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.airDash = 1; //空中冲刺次数恢复
        player.doubleJump = 1; //二段跳次数恢复
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        player.zeroVelocity();

        if (player.isGroundDetected()) //下地
        {
            if (Input.GetKeyDown(KeyCode.F)) 
            {
                stateMachine.changeState(player.idleState);
            }
        }

        if((!player.isWallDetected() && !player.isGroundDetected()) || !player.canClimb) //无墙掉落,体力没了掉落
        {
            stateMachine.changeState(player.fallState);
        }

        if (yInput != 0) //上下攀爬
        {
            rb.velocity = new Vector2(0, player.climbSpeed * yInput);
            if(player.curStamina > 0)
            {
                player.curStamina -= Time.deltaTime * player.staminaDecrease; //体力减少
            }
            else
            {
                player.curStamina = 0;
                player.canClimb = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && player.curStamina >= 15f) //大于消耗才能跳
        {
            if (yInput != 0)
            {
                stateMachine.changeState(player.wallVerticalJumpState);
            }
            else
            {
                stateMachine.changeState(player.wallJump);
            }
        }

        if (!player.isWallDetected() && player.isBottomWallDetected())
        {
            stateMachine.changeState(player.wallClimbState);
        }
    }
}
