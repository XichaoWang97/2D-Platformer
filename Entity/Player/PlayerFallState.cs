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
                player.audioController.PlaySfx(player.audioController.touchGround); //��Ч
                stateMachine.changeState(player.decelerateState);
            }
            else
            {
                player.audioController.PlaySfx(player.audioController.touchGround); //��Ч
                stateMachine.changeState(player.idleState);
            }
        }

        if (player.isWallDetected() && player.canClimb) //��ǽ
            stateMachine.changeState(player.wallSlideState);

        if (player.isIceDetected()) //����
            stateMachine.changeState(player.iceSlideState);

        if (Input.GetKeyDown(KeyCode.Space) && player.doubleJump <= 0) //���ܽ��ж�����ʱ�����Ԥ���뻺��
        {
            player.bufferTimer = player.jumpBufferTime; //Ԥ�����ʱ������
        }
        if(player.bufferTimer >= 0)//Ԥ�����ʱ������
        {
            player.bufferTimer -= Time.deltaTime;
        }

    }
}
