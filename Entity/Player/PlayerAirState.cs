using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    protected float jumpSpeed; //起跳速度

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
        
        if (xInput != 0) //空中移动
        {
            if (jumpSpeed < player.moveSpeed) //小于陆地移动速度，则空中移动速度为陆地移动速度
            {
                player.setVelocity(xInput * player.moveSpeed * 0.8f, rb.velocity.y);
            }
            else //否则就是起跳时的速度
            {
                player.setVelocity(xInput * jumpSpeed, rb.velocity.y);
            }
        }

        /*if (player.isWallDetected() && player.canClimb) //爬墙
            stateMachine.changeState(player.wallSlideState);*/
        /*if(xInput == 0)
        {
            player.setVelocity(0, rb.velocity.y);
        }*/

        /*if (Input.GetKeyDown(KeyCode.S)) //强制下降
        {
            player.setVelocity(0, -4 * player.jumpForce);
        }*/


    }
}
