using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoublePosMoving : MonoBehaviour
{
    public float speed; //ƽ̨����
    public float waitTime; //�ȴ�ʱ��
    public Transform[] movePos; //�ƶ�����λ��

    private int i;

    private Transform playerDefTransform;

    void Start()
    {
        i = 1;
        playerDefTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, movePos[i].position, speed * Time.deltaTime); //���������ƶ�

        if (Vector2.Distance(transform.position, movePos[i].position) < 0.1f)  //���������ƶ�
        {
            if (waitTime < 0.0f)
            {
                if (i == 0)
                    i = 1;
                else
                    i = 0;

                waitTime = 0.5f;
            }
            else
                waitTime -= Time.deltaTime;
        }
    }
}
