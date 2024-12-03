using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    protected float jumpSpeed; //�����ٶ�

    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        jumpSpeed = Mathf.Abs(rb.velocityX);
    }

    public override void Exit()
    {
        base.Exit();
        jumpSpeed = 0;
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space) && player.doubleJump != 0)
        {
            player.doubleJump = 0;
            stateMachine.changeState(player.jumpState);
        }

        if(player.airDash > 0)
        {
            player.checkForDashInput();
        }
        
        if (xInput != 0) //�����ƶ�
        {
            if (jumpSpeed < player.moveSpeed) //С��½���ƶ��ٶȣ�������ƶ��ٶ�Ϊ½���ƶ��ٶ�
            {
                player.setVelocity(xInput * player.moveSpeed * 0.8f, rb.velocity.y);
            }
            else //�����������ʱ���ٶ�
            {
                player.setVelocity(xInput * jumpSpeed, rb.velocity.y);
            }
        }

        /*if (player.isWallDetected() && player.canClimb) //��ǽ
            stateMachine.changeState(player.wallSlideState);*/
        /*if(xInput == 0)
        {
            player.setVelocity(0, rb.velocity.y);
        }*/

        /*if (Input.GetKeyDown(KeyCode.S)) //ǿ���½�
        {
            player.setVelocity(0, -4 * player.jumpForce);
        }*/


    }
}
