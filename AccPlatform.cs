using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccPlatform : MonoBehaviour
{
    public float speed; //ƽ̨����
    public float waitTime; //�ȴ�ʱ��
    public Transform movePos; //�ƶ�����λ��
    private Transform originalPos; //��ʼλ��
    [SerializeField] private GameObject _player;
    private Player player;
    private Rigidbody2D rb;
    void Start()
    {
        originalPos.position = transform.position;
        player = _player.GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>(); //ƽ̨����
    }

    void Update()
    {
        if (player.isAccPlatformDetected())  //��ɫ��
        {
            transform.position = Vector2.MoveTowards(transform.position, movePos.position, speed * Time.deltaTime); //��ʼλ���ƶ���Ŀ��λ��
            player.setVelocity(rb.velocity.x, rb.velocity.y); //��ɫ�̳������ٶ�
        }

        if (transform.position == movePos.position)
        {
            waitTime -= Time.deltaTime;
            if (waitTime <= 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, originalPos.position, 0.3f * speed * Time.deltaTime); //��ȥ
            }
        }
    }
}
