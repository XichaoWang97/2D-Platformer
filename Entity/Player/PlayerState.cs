using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;
    private string animBoolName;
    protected float xInput;
    protected float yInput;
    protected Rigidbody2D rb;
    protected float stateTimer; //��ʱ����������ÿ��״̬���ʼ���趨״̬����ʱ��
    protected bool triggerCalled; //����������������������
    public PlayerState(Player _player, PlayerStateMachine _stateMachine,string _animBoolName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName; 
    }

    public virtual void Enter()
    {
        player.anim.SetBool(animBoolName,true);
        rb = player.rb;
        triggerCalled = false; //һ��ʼ����Ϊfalse
    }
    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        player.anim.SetFloat("yVelocity", rb.velocity.y);
    }
    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}
