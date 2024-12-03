using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    //Vector3 initVelocity; //���֮ǰ���ٶ�
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        //initVelocity = rb.velocity; //���֮ǰ���ٶ�
        player.audioController.PlaySfx(player.audioController.dash); //��Ч
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
        if (stateTimer < player.dashJump) //��̺������Ծ
        {
            if (Input.GetKeyDown(KeyCode.Space) && player.isGroundDetected()) //������ܳ����
            {
                Debug.Log("dashjump");
                stateMachine.changeState(player.jumpState);
            }
        }

        if (stateTimer < 0) //���ʱ�����
        {
            //player.setVelocity(initVelocity.x, initVelocity.y); //�ٶȻָ��ɳ��֮ǰ��
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
        if(player.isGroundDetected())  //r.bvelocity.y,�޸��˾Ͳ���dashjump
        {
            player.setVelocity(player.dashSpeed * player.dashDir, rb.velocity.y);
        }
        else
        {
            player.setVelocity(player.dashSpeed * player.dashDir, 0);
        }

        /*if(player.dashDiry != 0 && player.dashDirx != 0) //����ٶ��Լ�����
        {
            player.setVelocity(player.dashSpeed * player.dashDirx * 0.7f, player.dashSpeed * player.dashDiry * 0.7f);
        }
        else
        {
            player.setVelocity(player.dashSpeed * player.dashDirx, player.dashSpeed * player.dashDiry);
        }*/

        if (player.isWallDetected() && !player.isGroundDetected()) //���г��ճ��ǽ��
        {
            stateMachine.changeState(player.wallSlideState);
        }
        if (player.isWallDetected() && player.isGroundDetected()) //�����̵�ǽ���г�idle
        {
            stateMachine.changeState(player.idleState);
        }
        if (player.isIceDetected()) //����
            stateMachine.changeState(player.iceSlideState);
    }
}
