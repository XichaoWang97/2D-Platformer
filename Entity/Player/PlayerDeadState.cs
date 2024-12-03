using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeadState : PlayerState
{
    public PlayerDeadState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }


    public override void Enter()
    {
        base.Enter();
        //GameObject.Find("Canvas").GetComponent<UI>().SwitchOnEndScreen();
        //player.Restart();
    }

    public override void Exit()
    {
        base.Exit();
        //Time.timeScale = 0f; //À¿ÕˆΩ· ¯∂≥Ω·“ª«–
    }

    public override void Update()
    {
        base.Update();
        player.setVelocity(0,0);
    }
}
