using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShadowSprite : MonoBehaviour
{
    private Transform player;
    private SpriteRenderer thissprite; //当前的sprite
    private SpriteRenderer playersprite; //玩家的sprite
    private Color color;

    [Header("时间控制参数")]
    public float activetime; //允许存在的时间
    public float activestart; //开始的时间

    [Header("不透明度参数")]
    private float alpha;
    public float alphamul; //变化的速度
    public float alphaset; //初始值

    private void OnEnable()
    {
        player = GameObject.Find("player").transform;
        thissprite = GetComponent<SpriteRenderer>();
        thissprite.sortingOrder = 100;
        playersprite = player.GetComponent<SpriteRenderer>();
        alpha = alphaset; //设置初始值
        thissprite.sprite = playersprite.sprite; //给贴图赋值
        transform.position = player.position;
        transform.localScale = player.localScale;
        transform.rotation = player.rotation; //把位置 旋转 大小缩放都同步
        activestart = Time.time; //实时记录开始的时间
    }

    private void Update()
    {
        alpha = alpha * alphamul; //残影的不透明度越来越大
        color = new Color(1, 1, 1, alpha);
        thissprite.color = color; //颜色

        if (Time.time > activestart + activetime)
        {
            ShadowPool._instance.returnpool(this.gameObject);  //返回对象池
        }
    }
}
