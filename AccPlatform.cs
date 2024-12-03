using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccPlatform : MonoBehaviour
{
    public float speed; //平台移速
    public float waitTime; //等待时间
    public Transform movePos; //移动到的位置
    private Transform originalPos; //初始位置
    [SerializeField] private GameObject _player;
    private Player player;
    private Rigidbody2D rb;
    void Start()
    {
        originalPos.position = transform.position;
        player = _player.GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>(); //平台刚体
    }

    void Update()
    {
        if (player.isAccPlatformDetected())  //角色在
        {
            transform.position = Vector2.MoveTowards(transform.position, movePos.position, speed * Time.deltaTime); //初始位置移动到目标位置
            player.setVelocity(rb.velocity.x, rb.velocity.y); //角色继承物体速度
        }

        if (transform.position == movePos.position)
        {
            waitTime -= Time.deltaTime;
            if (waitTime <= 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, originalPos.position, 0.3f * speed * Time.deltaTime); //回去
            }
        }
    }
}
