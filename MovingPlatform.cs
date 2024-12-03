using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed; //平台移速
    public float waitTime; //等待时间
    public Transform[] movePos; //移动到的位置

    private int i;
    [SerializeField]private Transform playerDefTransform;

    void Start()
    {
        i = 1;
        playerDefTransform = GameObject.FindGameObjectWithTag("Player").transform.parent;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, movePos[i].position, speed * Time.deltaTime); //两点来回移动

        if (Vector2.Distance(transform.position, movePos[i].position) < 0.1f)  //两点来回移动
        {
            if(waitTime < 0.0f)
            {
                if(i == 0)
                    i = 1;
                else
                    i = 0;

                waitTime = 0.5f;
            }
            else
                waitTime -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.BoxCollider2D")
            collision.gameObject.transform.parent = gameObject.transform;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.BoxCollider2D")
            collision.gameObject.transform.parent = playerDefTransform;
    }
}
