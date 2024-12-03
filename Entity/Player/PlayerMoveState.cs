using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    private float initSpeed;
    private float speed;
    public PlayerMoveState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        initSpeed = Mathf.Abs(rb.velocityX);
        speed = initSpeed;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        

        if (speed < player.moveSpeed) //速度爬升
        {
            speed += player.acc * Time.deltaTime;
        }
        if (speed > player.moveSpeed) //速度下降
        {
            speed = Mathf.Max(player.moveSpeed, speed - 2 * player.acc * Time.deltaTime);
        }
        if (xInput == 0) //不动减速
        {
            speed = Mathf.Max(0, speed - 4 * player.acc * Time.deltaTime);
            if(speed == 0)
            {
                stateMachine.changeState(player.idleState);
            }
        }

        player.setVelocity(xInput * speed, rb.velocity.y);
    }
}
