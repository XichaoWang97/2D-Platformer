using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIceSlideState : PlayerState
{
    private float speedX;
    public PlayerIceSlideState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if(Mathf.Abs(rb.velocity.x) > player.moveSpeed)
        {
            speedX = rb.velocity.x;
        }
        else
            speedX = player.moveSpeed * player.facingDir;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        player.setVelocity(1.2f * speedX, rb.velocity.y);

        /*if (!player.isIceDetected() && player.isGroundDetected())
        {
            stateMachine.changeState(player.decelerateState);
        }*/
        if (!player.isIceDetected())
        {
            stateMachine.changeState(player.fallState);
        }

        /*if (!player.isIceDetected())
        {
            stateMachine.changeState(player.decelerateState);
            /*if (player.isGroundDetected())
                stateMachine.changeState(player.idleState);
            else
                stateMachine.changeState(player.airState);
        }*/

        /*if (Input.GetKeyDown(KeyCode.Space) && player.isIceDetected()) //±ùÃæÆðÌø
        {
            stateMachine.changeState(player.jumpState);
        }*/
    }
}
