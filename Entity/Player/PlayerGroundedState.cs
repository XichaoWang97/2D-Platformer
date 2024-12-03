using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    private float timer; //���Ǽ�ʱ��
    
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        timer = player.coyoteTime; //��������������ʱ��
        player.airDash = 1; //���г�̴����ָ�
        player.doubleJump = 1; //�����������ָ�
        player.StartCoroutine(CanClimb());
    }

    IEnumerator CanClimb() //��2s������
    {
        yield return new WaitForSeconds(2);
        player.canClimb = true;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        
        if (player.curStamina < player.stamina)
        {
            player.curStamina += player.staminaIncrease * Time.deltaTime; //���������ָ�
        }
        else
        {
            player.curStamina = player.stamina;
        }
            

        if (player.isWallDetected() && player.canClimb)
        {
            if (Input.GetKeyDown(KeyCode.F))  //��f��ʼ��
            {
                stateMachine.changeState(player.wallSlideState);
            }
        }

        if (player.isIceDetected())
            stateMachine.changeState(player.iceSlideState);

        if (!player.isGroundDetected()) //�������������
        {
            timer -= Time.deltaTime;
            if(timer <= 0) //��ʱ������Ž���airState
            {
                stateMachine.changeState(player.fallState);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space)) // ��Ծ
        {
            stateMachine.changeState(player.jumpState);
        }
        

        if (player.bufferTimer > 0) //������Ծ�������Ծ
        {
            stateMachine.changeState(player.jumpState);
        }

        player.checkForDashInput(); //���
    }
}
