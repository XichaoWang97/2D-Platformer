using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    #region components
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public SpriteRenderer sr { get; private set; }
    public int facingDir { get; private set; } = 1;
    protected bool facingRight = true;
    public System.Action onFlipped;
    public CapsuleCollider2D cd { get; private set; }
    #endregion

    [Header("collision_info")]
    [SerializeField] protected float CheckDistance;
    
    [SerializeField] protected LayerMask collision;
    [SerializeField] protected LayerMask whatIsWall;
    [SerializeField] protected LayerMask whatIsIce;
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected LayerMask whatIsAccPlatform;

    [Header("knockback_info")]  //击退
    [SerializeField] protected Vector2 knockbackDir;
    [SerializeField] protected float knockbackDuration;
    protected bool isKnocked;

    [Header("Vertex_info")]
    [SerializeField] protected Vector3[] vertex;
    public float offsetIndex = 0.2f; //偏移因子，偏移四角用的
    protected virtual void Awake()
    {

    }
    protected virtual void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        cd = GetComponent<CapsuleCollider2D>();
    }

    protected virtual void Update()
    {
        vertex = GetCornersForBoxCollider(offsetIndex);

        /*if (isMovingPlatformDetected()) //跟随移动平台动
        {
            transform.position = Vector3.zero;
        }*/
    }

    #region collision
    public virtual bool isGroundDetected() => 
        Physics2D.Raycast(vertex[0], Vector2.down, CheckDistance, whatIsGround) || Physics2D.Raycast(vertex[1], Vector2.down, CheckDistance, whatIsGround);
    public virtual bool isWallDetected() => 
        (Physics2D.Raycast(vertex[9], Vector2.right, CheckDistance, whatIsWall) && facingDir == 1) 
        || (Physics2D.Raycast(vertex[11], Vector2.left, CheckDistance, whatIsWall) && facingDir == -1);
    public virtual bool isBottomWallDetected() => 
        (Physics2D.Raycast(vertex[1], Vector2.right, CheckDistance, whatIsWall) && facingDir == 1)
        || (Physics2D.Raycast(vertex[0], Vector2.left, CheckDistance, whatIsWall) && facingDir == -1);
    public virtual bool isIceDetected() => 
        Physics2D.Raycast(vertex[0], Vector2.down, CheckDistance, whatIsIce) || Physics2D.Raycast(vertex[1], Vector2.down, CheckDistance, whatIsIce);
    public virtual bool isAccPlatformDetected() => //移动平台检测
        Physics2D.Raycast(vertex[0], Vector2.down, CheckDistance, whatIsAccPlatform)   //站在上面
        || Physics2D.Raycast(vertex[1], Vector2.down, CheckDistance, whatIsAccPlatform) || //或者
        (Physics2D.Raycast(vertex[9], Vector2.right, CheckDistance, whatIsAccPlatform) && facingDir == 1) //爬在侧面
        || (Physics2D.Raycast(vertex[11], Vector2.left, CheckDistance, whatIsAccPlatform) && facingDir == -1);
    

    //不严格检测
    public virtual bool isUp() => 
        Physics2D.Raycast(vertex[8], Vector2.up, CheckDistance, collision) || Physics2D.Raycast(vertex[10], Vector2.up, CheckDistance, collision);
    public virtual bool isDown() => 
        Physics2D.Raycast(vertex[4], Vector2.down, CheckDistance, collision) || Physics2D.Raycast(vertex[6], Vector2.down, CheckDistance, collision);
    public virtual bool isRight() => 
        Physics2D.Raycast(vertex[7], Vector2.right, CheckDistance, collision) || Physics2D.Raycast(vertex[9], Vector2.right, CheckDistance, collision);
    public virtual bool isLeft() => 
        Physics2D.Raycast(vertex[2], Vector2.left, CheckDistance, collision) || Physics2D.Raycast(vertex[11], Vector2.left, CheckDistance, collision);

    //严格检测
    public virtual bool v2Up() => Physics2D.Raycast(vertex[2], Vector2.up, CheckDistance, collision);
    public virtual bool v3Up() => Physics2D.Raycast(vertex[3], Vector2.up, CheckDistance, collision);
    public virtual bool v0Down() => Physics2D.Raycast(vertex[0], Vector2.down, CheckDistance, collision); 
    public virtual bool v1Down() => Physics2D.Raycast(vertex[1], Vector2.down, CheckDistance, collision);
    public virtual bool v1Right() => Physics2D.Raycast(vertex[1], Vector2.right, CheckDistance, collision);
    public virtual bool v2Right() => Physics2D.Raycast(vertex[2], Vector2.right, CheckDistance, collision);
    public virtual bool v0Left() => Physics2D.Raycast(vertex[0], Vector2.left, CheckDistance, collision); 
    public virtual bool v3Left() => Physics2D.Raycast(vertex[3], Vector2.left, CheckDistance, collision);

    protected virtual void OnDrawGizmos()
    {
        //4顶点8线检测
        /*Gizmos.DrawLine(vertex[0], new Vector3(vertex[0].x - CheckDistance, vertex[0].y));
        Gizmos.DrawLine(vertex[0], new Vector3(vertex[0].x, vertex[0].y - CheckDistance));
        Gizmos.DrawLine(vertex[1], new Vector3(vertex[1].x + CheckDistance, vertex[1].y));
        Gizmos.DrawLine(vertex[1], new Vector3(vertex[1].x, vertex[1].y - CheckDistance));*/
        //Gizmos.DrawLine(vertex[2], new Vector3(vertex[2].x + CheckDistance, vertex[2].y));
        Gizmos.DrawLine(vertex[2], new Vector3(vertex[2].x, vertex[2].y + CheckDistance));
        //Gizmos.DrawLine(vertex[3], new Vector3(vertex[3].x - CheckDistance, vertex[3].y));
        Gizmos.DrawLine(vertex[3], new Vector3(vertex[3].x, vertex[3].y + CheckDistance));
        //8检测点检测
        /*Gizmos.DrawLine(vertex[4], new Vector3(vertex[4].x, vertex[4].y - CheckDistance));
        Gizmos.DrawLine(vertex[5], new Vector3(vertex[5].x - CheckDistance, vertex[5].y));
        Gizmos.DrawLine(vertex[6], new Vector3(vertex[6].x, vertex[6].y - CheckDistance));
        Gizmos.DrawLine(vertex[7], new Vector3(vertex[7].x + CheckDistance, vertex[7].y));*/
        Gizmos.DrawLine(vertex[8], new Vector3(vertex[8].x, vertex[8].y + CheckDistance));
        //Gizmos.DrawLine(vertex[9], new Vector3(vertex[9].x + CheckDistance, vertex[9].y));
        Gizmos.DrawLine(vertex[10], new Vector3(vertex[10].x, vertex[10].y + CheckDistance));
        //Gizmos.DrawLine(vertex[11], new Vector3(vertex[11].x - CheckDistance, vertex[11].y));
    }
    #endregion

    #region flip
    public void flip() //翻转
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);

        if(onFlipped != null)
            onFlipped();
    }
    public void flipController(float _x)
    {
        if (_x < 0 && facingRight)
        {
            flip();
        }
        else if (_x > 0 && !facingRight)
        {
            flip();
        }
    }
    #endregion

    #region velocity
    public void setVelocity(float _xVelocity, float _yVelocity)
    {
        if (isKnocked)
            return;

        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        flipController(_xVelocity);
    }

    public void zeroVelocity(){
        if (isKnocked)
            return;

        rb.velocity = new Vector2(0, 0);
    } 
    #endregion

    protected virtual IEnumerator HitKnockback(){     //击退
        isKnocked = true;
        rb.velocity = new Vector2(knockbackDir.x * -facingDir, knockbackDir.y);

        yield return new WaitForSeconds(knockbackDuration);
        isKnocked = false;
    }

    Vector3[] GetCornersForBoxCollider(float index)
    {
        Vector3[] verts = new Vector3[12];
        CapsuleCollider2D b = GetComponent<CapsuleCollider2D>(); //retrieves the Collider of the GameObject called obj
        //4角顶点
        verts[0] = new Vector3(transform.position.x - b.size.x * 0.5f, transform.position.y - b.size.y * 0.5f); //左下
        verts[1] = new Vector3(transform.position.x + b.size.x * 0.5f, transform.position.y - b.size.y * 0.5f); //右下
        verts[2] = new Vector3(transform.position.x + b.size.x * 0.5f, transform.position.y + b.size.y * 0.5f); //右上
        verts[3] = new Vector3(transform.position.x - b.size.x * 0.5f, transform.position.y + b.size.y * 0.5f); //左上

        //8检测点
        verts[4] = verts[0] + new Vector3(b.size.x * index, 0);
        verts[5] = verts[0] + new Vector3(0, b.size.y * index);

        verts[6] = verts[1] - new Vector3(b.size.x * index, 0);
        verts[7] = verts[1] + new Vector3(0, b.size.y * index);

        verts[8] = verts[2] - new Vector3(b.size.x * index, 0);
        verts[9] = verts[2] - new Vector3(0, b.size.y * index);

        verts[10] = verts[3] + new Vector3(b.size.x * index, 0);
        verts[11] = verts[3] - new Vector3(0, b.size.y * index);
        return verts;
    }

    #region fixedPosition
    public void FixedPosition_Up(float v) //上方碰撞位置修复
    {
        if (v3Up() && !v2Up())
        {
            if (!isUp())
                transform.position = new Vector3(transform.position.x + v * Time.deltaTime, transform.position.y);
        }
        if (v2Up() && !v3Up())
        {
            if (!isUp())
                transform.position = new Vector3(transform.position.x - v * Time.deltaTime, transform.position.y);
        }
    }
    /*public void FixedPosition_Down(float v) //下方碰撞位置修复
    {
        if (v0Down() && !v1Down())
        {
            if (!isDown())
            {
                Debug.Log("down");
                transform.position = new Vector3(transform.position.x + v * Time.deltaTime, transform.position.y);
            }
        }
        if (v1Down() && !v0Down())
        {
            if (!isDown())
            {
                Debug.Log("down");
                transform.position = new Vector3(transform.position.x - v * Time.deltaTime, transform.position.y);
            }
        }
    }
    public void FixedPosition_Right(float v)
    {
        if (v1Right() && !v2Right())
        {
            if (!isRight())
            {
                Debug.Log("right");
                transform.position = new Vector3(transform.position.x, transform.position.y + v * Time.deltaTime);
            }
        }
        if (v2Right() && !v1Right())
        {
            if (!isRight())
            {
                Debug.Log("right");
                transform.position = new Vector3(transform.position.x, transform.position.y - v * Time.deltaTime);
            }
        }
    }
    public void FixedPosition_Left(float v)
    {
        if (v0Left() && !v3Left())
        {
            if (!isRight())
            {
                Debug.Log("left");
                transform.position = new Vector3(transform.position.x, transform.position.y + v * Time.deltaTime);
            }
        }
        if (!v0Left() && v3Left())
        {
            if (!isRight())
            {
                Debug.Log("left");
                transform.position = new Vector3(transform.position.x, transform.position.y - v * Time.deltaTime);
            }
        }
    }*/
    #endregion
}
