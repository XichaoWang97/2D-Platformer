using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerAirState
{
    
    public PlayerFallState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        Debug.Log("fall");
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (player.isGroundDetected())
        {
            if(Mathf.Abs(rb.velocity.x) > player.moveSpeed)
            {
                player.audioController.PlaySfx(player.audioController.touchGround); //音效
                stateMachine.changeState(player.decelerateState);
            }
            else
            {
                player.audioController.PlaySfx(player.audioController.touchGround); //音效
                stateMachine.changeState(player.idleState);
            }
        }

        if (player.isWallDetected() && player.canClimb) //爬墙
            stateMachine.changeState(player.wallSlideState);

        if (player.isIceDetected()) //滑冰
            stateMachine.changeState(player.iceSlideState);

        if (Input.GetKeyDown(KeyCode.Space) && player.doubleJump <= 0) //不能进行二段跳时则进入预输入缓存
        {
            player.bufferTimer = player.jumpBufferTime; //预输入计时器重置
        }
        if(player.bufferTimer >= 0)//预输入计时器走字
        {
            player.bufferTimer -= Time.deltaTime;
        }

    }
}
