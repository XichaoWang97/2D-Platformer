using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDecelerateState : PlayerGroundedState
{
    private float speed;
    private float initSpeed;
    public PlayerDecelerateState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        speed = rb.velocity.x;
        initSpeed = rb.velocity.x;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        speed -= 2 * player.facingDir * player.acc * Time.deltaTime;
        if(initSpeed > 0)
        {
            player.setVelocity(Mathf.Max(speed, 0), rb.velocity.y);
        }
        if(initSpeed < 0)
        {
            player.setVelocity(Mathf.Min(speed, 0), rb.velocity.y);
        }

        if (rb.velocityX == 0) //减没了进入idleState
        {
            stateMachine.changeState(player.idleState);
        }
    }
}
