using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoublePosMoving : MonoBehaviour
{
    public float speed; //平台移速
    public float waitTime; //等待时间
    public Transform[] movePos; //移动到的位置

    private int i;

    private Transform playerDefTransform;

    void Start()
    {
        i = 1;
        playerDefTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, movePos[i].position, speed * Time.deltaTime); //两点来回移动

        if (Vector2.Distance(transform.position, movePos[i].position) < 0.1f)  //两点来回移动
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
