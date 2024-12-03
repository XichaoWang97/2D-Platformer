using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Player : Entity
{
    #region states
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerDecelerateState decelerateState { get; private set; } //����״̬
    public PlayerFallState fallState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    //public PlayerDashJumpState dashJumpState { get; private set; } //�����Ծ
    public PlayerIceSlideState iceSlideState { get; private set; } //����
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerWallJumpState wallJump { get; private set; }
    public PlayerWallVerticalJumpState wallVerticalJumpState { get; private set; }
    public PlayerWallClimbState wallClimbState { get; private set; }
    public PlayerDeadState deadState { get; private set; }
    #endregion
    #region components
    public bool isBusy { get; private set; }

    [Header("move_info")]
    public float moveSpeed = 8f;
    public float coyoteTime = 0.1f; //����ʱ��
    public float acc = 40f; //����,����������2��
    public int moveDir;
    public float moveStartTime;

    [Header("jump_info")]
    public float jumpForce = 12f;
    public float jumpBufferTime = .1f; //��ԾԤ����ʱ��
    public float bufferTimer; //��ԾԤ�����ʱ��
    public float jumpHoldDuration = 0.2f; //��Ծ����ʱ��

    [Header("dash_info")]
    public float dashSpeed = 20f;
    public float dashDuration;
    /*public float dashDirx;
    public float dashDiry;*/
    public float dashDir;
    public float dashJump = .1f; //���β�˿�����Ծ

    [Header("air_info")]
    public int doubleJump = 1;
    public int airDash = 1;

    [Header("climb_info")]
    public float climbSpeed = 4f;
    public float stamina = 100; //����ֵ����
    public float curStamina = 100; //��ǰ����ֵ
    public float staminaDecrease; //������
    public float staminaIncrease; //������
    public bool canClimb = true;

    [Header("reborn_info")]
    public float dieTime;
    public RebornSiteManager rebornSiteManager;
    private Vector3 rebornTransform;
    public float rebornHeight;

    [Header("particle_info")]
    //public GameObject dust;
    public ParticleSystem dustFX;

    [Header("audio_info")]
    public AudioController audioController;
    #endregion

    protected override void Awake() //animator�������ע��
    {
        base.Awake();
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this,stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine,"Move");
        decelerateState = new PlayerDecelerateState(this, stateMachine, "Decelerate");

        fallState = new PlayerFallState(this, stateMachine, "Jump");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");

        dashState = new PlayerDashState(this, stateMachine, "Dash");
        //dashJumpState = new PlayerDashJumpState(this, stateMachine, "DashJump");

        iceSlideState = new PlayerIceSlideState(this, stateMachine, "IceSlide");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJump = new PlayerWallJumpState(this, stateMachine, "Jump");
        wallVerticalJumpState = new PlayerWallVerticalJumpState(this, stateMachine, "WallVJump");
        wallClimbState = new PlayerWallClimbState(this, stateMachine, "WallClimb");
        deadState = new PlayerDeadState(this, stateMachine, "Die");
    }

    protected override void Start()
    {   
        base.Start();
        stateMachine.Initialize(idleState);
        //audioController = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioController>();
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();

        if (Mathf.Abs(rb.velocity.x) > moveSpeed) //�����ƶ��ٶ����в�Ӱ
        {
            ShadowPool._instance.GetFormPool();
        }
        if (Mathf.Abs(rb.velocity.y) > jumpForce) //������Ծ�����в�Ӱ
        {
            ShadowPool._instance.GetFormPool();
        }

    }

    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;
        yield return new WaitForSeconds(_seconds);
        isBusy = false;
    }

    #region dash
    public virtual void checkForDashInput()  //��̼���ָ��
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            dashDir = Input.GetAxisRaw("Horizontal");
            if (dashDir == 0)
            {
                dashDir = facingDir;
            }
            /*dashDirx = Input.GetAxisRaw("Horizontal");
            dashDiry = Input.GetAxisRaw("Vertical");
            if (dashDirx == 0 && dashDiry == 0)
            {
                dashDirx = facingDir;
            }*/
            stateMachine.changeState(dashState);
            if(!isGroundDetected()) //���ڵ��棬����г�̴���-1
            {
                airDash -= 1;
            }
        }
    }
    #endregion

   private void OnCollisionEnter2D(Collision2D collision) //������������
    {
        if (collision.gameObject.CompareTag("trap"))
        {
            StartCoroutine("Die");
            //stateMachine.changeState(deadState);
        }
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("trap"))
        {
            StopCoroutine("Die");
        }
    }
    IEnumerator Die()
    {
        yield return new WaitForSeconds(dieTime);
        stateMachine.changeState(deadState);
    }

    public void Reborn()
    {
        for (int i = 0; i < rebornSiteManager.rebornSites.Length; i++) //��������㣬������µĵ�
        {
            if(rebornSiteManager.rebornSites[i].isActive == true)
            {
                rebornTransform = rebornSiteManager.rebornSites[i].transform.position;
            }
        }
        //anim.SetTrigger("reborn"); //��������
        transform.position = rebornTransform + new Vector3(0, rebornHeight, 0);
        stateMachine.changeState(idleState);
    }

    void CheckHorzontalMove() //ˮƽ�˶���������,��bug��δʹ��
    {
        //<= 0˵�������������û�а������ж���ʱ�Ƿ������Ҽ�
        if (Input.GetKeyDown(KeyCode.D) && Input.GetAxisRaw("Horizontal") <= 0)
        {
            moveDir = 1;
        }
        if (Input.GetKeyDown(KeyCode.A) && Input.GetAxisRaw("Horizontal") >= 0)
        {
            moveDir = -1;
        }

        if (Input.GetKeyUp(KeyCode.D))  //�жϷſ��Ҽ�ʱ�������Ƿ�����
        {
            if (Input.GetKey(KeyCode.A))  //�ſ��Ҽ���ʱ���԰������
            {
                moveDir = -1;
                moveStartTime = Time.time;
            }
            else
            {
                moveDir = 0;
            }
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            if (Input.GetKey(KeyCode.D))
            {
                moveDir = 1;
                moveStartTime = Time.time;
            }
            else
            {
                moveDir = 0;
            }
        }
    }

    public void CreateDust() //���Żҳ�������Ч
    {
        dustFX.Play();
    }

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

}
