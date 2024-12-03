using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAirState
{
    float jumpTimer = 0; //������ʱ��
    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        Debug.Log("jump");
        base.Enter();
        player.CreateDust(); //��Ծ�ҳ�
        player.audioController.PlaySfx(player.audioController.jump); //��Ч
        rb.velocity = new Vector2(rb.velocity.x, player.jumpForce);
        Debug.Log("player.jumpForce = "+ player.jumpForce);
        Debug.Log("rb.velocity.y = "+ rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
        jumpTimer = 0;
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKey(KeyCode.Space) && !player.isGroundDetected() && jumpTimer <= player.jumpHoldDuration) // ���Գ�������
        {
            jumpTimer += Time.deltaTime; //����
            rb.velocity = new Vector2(rb.velocity.x, player.jumpForce);
        }

        if (rb.velocity.y < 0)
        {
            stateMachine.changeState(player.fallState);
        }

        player.FixedPosition_Up(player.moveSpeed); //ͷ������
    }
}
