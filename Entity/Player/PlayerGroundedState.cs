using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    private float timer; //土狼计时器
    
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        timer = player.coyoteTime; //到地上则有土狼时间
        player.airDash = 1; //空中冲刺次数恢复
        player.doubleJump = 1; //二段跳次数恢复
        player.StartCoroutine(CanClimb());
    }

    IEnumerator CanClimb() //等2s才能爬
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
            player.curStamina += player.staminaIncrease * Time.deltaTime; //地面体力恢复
        }
        else
        {
            player.curStamina = player.stamina;
        }
            

        if (player.isWallDetected() && player.canClimb)
        {
            if (Input.GetKeyDown(KeyCode.F))  //按f开始爬
            {
                stateMachine.changeState(player.wallSlideState);
            }
        }

        if (player.isIceDetected())
            stateMachine.changeState(player.iceSlideState);

        if (!player.isGroundDetected()) //土狼跳相关设置
        {
            timer -= Time.deltaTime;
            if(timer <= 0) //计时器走完才进入airState
            {
                stateMachine.changeState(player.fallState);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space)) // 跳跃
        {
            stateMachine.changeState(player.jumpState);
        }
        

        if (player.bufferTimer > 0) //存在跳跃缓冲的跳跃
        {
            stateMachine.changeState(player.jumpState);
        }

        player.checkForDashInput(); //冲刺
    }
}
