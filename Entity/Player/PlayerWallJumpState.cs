using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerAirState //除了enter速度，其余和jump一样
{
    float jumpTimer = 0;
    public PlayerWallJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.CreateDust(); //跳跃灰尘
        player.audioController.PlaySfx(player.audioController.jump); //音效
        player.setVelocity(player.moveSpeed * -player.facingDir, player.jumpForce);
        player.curStamina -= 15f; //体力减少
    }

    public override void Exit()
    {
        base.Exit();
        jumpTimer = 0;
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKey(KeyCode.Space) && !player.isGroundDetected() && jumpTimer <= player.jumpHoldDuration)
        {
            jumpTimer += Time.deltaTime;
            rb.velocity = new Vector2(rb.velocity.x, player.jumpForce);
        }

        if (rb.velocity.y < 0)
        {
            stateMachine.changeState(player.fallState);
        }

        player.FixedPosition_Up(player.moveSpeed);
    }
}
