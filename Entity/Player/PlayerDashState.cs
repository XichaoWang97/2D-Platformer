using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    //Vector3 initVelocity; //冲刺之前的速度
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        //initVelocity = rb.velocity; //冲刺之前的速度
        player.audioController.PlaySfx(player.audioController.dash); //音效
        stateTimer = player.dashDuration;
    }

    public override void Exit()
    {
        base.Exit();
        //player.setVelocity(initVelocity.x, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < player.dashJump) //冲刺后可以跳跃
        {
            if (Input.GetKeyDown(KeyCode.Space) && player.isGroundDetected()) //地面才能冲刺跳
            {
                Debug.Log("dashjump");
                stateMachine.changeState(player.jumpState);
            }
        }

        if (stateTimer < 0) //冲刺时间结束
        {
            //player.setVelocity(initVelocity.x, initVelocity.y); //速度恢复成冲刺之前的
            //stateMachine.changeState(player.idleState);
            if (player.isGroundDetected())
            {
                stateMachine.changeState(player.idleState);
            }
            else
            {
                stateMachine.changeState(player.fallState);
            }
        }
        if(player.isGroundDetected())  //r.bvelocity.y,修改了就不能dashjump
        {
            player.setVelocity(player.dashSpeed * player.dashDir, rb.velocity.y);
        }
        else
        {
            player.setVelocity(player.dashSpeed * player.dashDir, 0);
        }

        /*if(player.dashDiry != 0 && player.dashDirx != 0) //冲刺速度以及方向
        {
            player.setVelocity(player.dashSpeed * player.dashDirx * 0.7f, player.dashSpeed * player.dashDiry * 0.7f);
        }
        else
        {
            player.setVelocity(player.dashSpeed * player.dashDirx, player.dashSpeed * player.dashDiry);
        }*/

        if (player.isWallDetected() && !player.isGroundDetected()) //空中冲刺粘在墙上
        {
            stateMachine.changeState(player.wallSlideState);
        }
        if (player.isWallDetected() && player.isGroundDetected()) //地面冲刺到墙上切成idle
        {
            stateMachine.changeState(player.idleState);
        }
        if (player.isIceDetected()) //滑冰
            stateMachine.changeState(player.iceSlideState);
    }
}
